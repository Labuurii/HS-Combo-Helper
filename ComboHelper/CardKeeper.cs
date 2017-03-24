using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboHelper
{
    enum Alliance
    {
        unknown,
        unchanged,
        enemy,
        friend
    }

    struct CategorizedCard
    {
        internal Alliance alliance;
        internal MemoryZone last_zone;
        internal MemoryCard card;

        internal CategorizedCard(MemoryCard card)
        {
            last_zone = MemoryZone.Unknown;
            this.card = card;
            alliance = Alliance.unknown;
        }

        public override bool Equals(object obj)
        {
            return card.Equals(obj);
        }

        public override int GetHashCode()
        {
            return card.GetHashCode();
        }

        public override string ToString()
        {
            return card.name + " " + last_zone + ", " + card.zone + ", " +  alliance + ", " + card.game_id;
        }
    }

    internal class CardKeeper
    {
        Alliance[][] alliance_transition_table = new Alliance[][]
        {
                                //hand              //play              //deck              //dead              //unknown
            new Alliance[] {Alliance.friend,    Alliance.friend,    Alliance.friend,    Alliance.friend,    Alliance.friend     },      //hand
            new Alliance[] {Alliance.friend,    Alliance.unchanged, Alliance.enemy,     Alliance.unknown,   Alliance.enemy      },      //play
            new Alliance[] {Alliance.friend,    Alliance.unchanged, Alliance.unchanged, Alliance.unknown,   Alliance.unknown    },      //deck
            new Alliance[] {Alliance.friend,    Alliance.unchanged, Alliance.unchanged, Alliance.unchanged, Alliance.unknown    },      //dead      
            new Alliance[] {Alliance.unknown,   Alliance.unknown,   Alliance.unknown,   Alliance.unknown,   Alliance.unknown    }       //unknown
        };

        Func<CategorizedCard, int>[][] deck_count_transition_table = new Func<CategorizedCard, int>[][]
        {
                                                    //hand                 //play                        //deck                              //dead                     //unknown
            new Func<CategorizedCard, int>[] {      no_change,           no_change,                     static_dec,                         no_change,                  static_dec                  },      //hand
            new Func<CategorizedCard, int>[] {      no_change,           no_change,                     possibly_from_our_cart,             no_change,                  possibly_from_our_cart      },      //play
            new Func<CategorizedCard, int>[] {      static_inc,          possibly_from_our_cart,        no_change,                          possibly_from_our_cart,     no_change                   },      //deck
            new Func<CategorizedCard, int>[] {      no_change,           no_change,                     possibly_from_our_cart,             no_change,                  no_change                   },      //dead      
            new Func<CategorizedCard, int>[] {      no_change,           no_change,                     no_change,                          no_change,                  no_change                   }       //unknown
        };

        private List<CategorizedCard> categorized_cards = new List<CategorizedCard>();
        int deck_card_count = 30;

        internal IReadOnlyList<CategorizedCard> CategorizedCards
        {
            get
            {
                return categorized_cards;
            }
        }

        internal int DeckCount
        {
            get
            {
                return deck_card_count;
            }
        }

        internal void Reset()
        {
            categorized_cards.Clear();
            deck_card_count = 30;
        }

        internal void CategorizeBoard(Board board)
        {
            categorize_board_impl(board.Cards);

            for(var i = 0; i < categorized_cards.Count; ++i)
            {
                var card = categorized_cards[i];
                update_deck_count(card);
                categorized_cards[i] = update_categorization(card);
            }
        }

        private static int no_change(CategorizedCard card)
        {
            return 0;
        }

        private static int possibly_from_our_cart(CategorizedCard card)
        {
            switch(card.alliance)
            {
                case Alliance.enemy:
                    return 0;
                case Alliance.friend:
                    return -1;
                case Alliance.unknown:
                    return 0;
                default:
                    Debug.Assert(false);
                    return 0;
            }
        }

        private static int static_dec(CategorizedCard card)
        {
            return -1;
        }

        private static int static_inc(CategorizedCard card)
        {
            return 1;
        }

        private CategorizedCard update_categorization(CategorizedCard categorized_card)
        {
            var current_zone = categorized_card.card.zone;
            var last_zone = categorized_card.last_zone;
            var alliance = alliance_transition_table[(int)current_zone][(int)last_zone];

            if (alliance != Alliance.unchanged)
                categorized_card.alliance = alliance;
            return categorized_card;
        }

        private void update_deck_count(CategorizedCard categorized_card)
        {
            var current_zone = categorized_card.card.zone;
            var last_zone = categorized_card.last_zone;
            var deck_count_change = deck_count_transition_table[(int)current_zone][(int)last_zone](categorized_card);
            deck_card_count += deck_count_change;

        }

        private void categorize_board_impl(IList<MemoryCard> cards)
        {
            foreach(var card in cards)
            {
                var idx = -1;

                //This is useful because List.IndexOf does not allow arbitrary objects
                for(var i = 0; i < categorized_cards.Count; ++i)
                {
                    if (categorized_cards[i].Equals(card))
                    {
                        idx = i;
                        break;
                    }
                }

                if(idx == -1)
                {
                    categorized_cards.Add(new CategorizedCard(card));

                }
                else
                {
                    var categorized_card = categorized_cards[idx];
                    categorized_card.last_zone = categorized_card.card.zone;
                    categorized_card.card = card;
                    categorized_card.card.zone = card.zone;
                    categorized_cards[idx] = categorized_card;
                }
            }
        }

        private static int belonging_score(MemoryZone zone)
        {
            //Only reason this exists is because I do not want to break state machine transition tables

            switch(zone)
            {
                case MemoryZone.GRAVEYARD:
                    return 0;
                case MemoryZone.PLAY:
                    return 1;
                case MemoryZone.HAND:
                    return 2;
                case MemoryZone.DECK:
                    return 3;
                case MemoryZone.Unknown:
                    return 4;

                default:
                    Debug.Assert(false);
                    return -1;
            }
        }

        internal static void FilterAndAssumeBelongings(Board board)
        {
            //Sometimes cards are not removed from memory.
            //Because they are just strings and this game uses C#
            //This means there can be two memory cards defining the same id in two zones
            //Therefore we assume that the card have got goes from deck to hand to play to graveyard
            //Meaning if the card exist in graveyard, it can not exist anywhere else.
            //If it is in play then it can not exist in hand or deck.
            //And if it exists in hand it can not exist in deck.
            //Of course this is not always true but it is good enough hopefully

            var cards = board.Cards;
            cards.Sort(delegate(MemoryCard a, MemoryCard b){
                var game_id_cmp = a.game_id.CompareTo(b.game_id);
                if (game_id_cmp == 0)
                    return belonging_score(a.zone).CompareTo(belonging_score(b.zone));
                else
                    return game_id_cmp;
            });

            for(var i = 0; i < cards.Count; ++i)
            {
                var first_card = cards[i];

                if(first_card.zone == MemoryZone.PLAY && first_card.zone_pos == 0)
                {
                    cards.RemoveAt(i);
                    --i;
                    continue;
                }

                for (var j = i + 1; j < cards.Count; ++j)
                {
                    var latent_card = cards[j];
                    if (latent_card.game_id != first_card.game_id)
                        break;
                    cards.RemoveAt(j);
                }

                    /*
                    var bel_score = belonging_score(first_card.zone);
                    for(var j = i + 1; j < cards.Count; ++j)
                    {
                        var latent_card = cards[j];
                        if (latent_card.game_id != first_card.game_id)
                            break;

                        var latent_bel_score = belonging_score(latent_card.zone);
                        if(bel_score > latent_bel_score)
                        {
                            //Remove latent
                            cards.RemoveAt(j);
                        }
                        else
                        {
                            //Remove current and continue loop
                            cards.RemoveAt(i);
                            --i;
                            //TODO: Goto bottom of loop
                            break;
                        }
                    }
                    */

                    /*
                    for(var j = i + 1; j < cards.Count; ++j)
                    {
                        var latent_card = cards[j];
                        if(first_card.game_id == latent_card.game_id)
                        {
                            if (first_card.zone_pos < latent_card.zone_pos)
                            {
                                cards.RemoveAt(j);
                            } else
                            {
                                cards.RemoveAt(i);
                                --i;
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    */
                }
        }

        private static void filter_duplicate_ids(IList<int> ids)
        {
            for (var i = 0; i < ids.Count;++i)
            {
                for (var j = 0; j < ids.Count; ++j)
                {
                    if (i == j) continue;

                    if (ids[i].Equals(ids[j]))
                    {
                        ids.RemoveAt(j);
                        --i;
                        break;
                    }
                }
            }
        }
    }
}
