using BotPlugin.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Drawing;
using System.Threading;
using ComboHelper.GUI;
using System.Diagnostics;

namespace ComboHelper
{
    enum TrackDisruption
    {
        Yes,
        No
    }

    internal class GameKeeper
    {
        IWindow hs;
        Overlay overlay = new Overlay();
        Board board;
        List<Combo> combos;
        CardKeeper card_keeper = new CardKeeper();
        int disruption_count;

        const int MAX_TOLERABLE_DISRUPTION = 15;

        internal GameKeeper(IWindow hs, Board board, List<Combo> combos)
        {
            this.hs = hs;
            this.board = board;
            this.combos = combos;
        }

        internal void bot_loop()
        {
            for (;;)
            {
                try
                {
                    wait_for_match_start_frame();

                    hs.AddOverlay(5, 10, overlay);
                    disruption_count = 0;
                    overlay.UpdateDisruption(0);

                    card_keeper.Reset();
                    update();

                    overlay.UpdateStatus("Waiting for match to start...");
                    wait_for_match_start();

                    overlay.UpdateStatus("Waiting for our turn");
                    wait_until_our_turn(TrackDisruption.Yes);
                    update();
                    
                    for (;;)
                    {
                        if (our_turn(TrackDisruption.Yes))
                        {
                            overlay.UpdateStatus("Our turn");

                            wait_until_green_turn_button_or_enemy_turn();
                            update();
                        }
                        else
                        {
                            overlay.UpdateStatus("Enemy turn");
                            wait_until_our_turn(TrackDisruption.Yes);
                            update();
                        }
                    }
                }
                catch(DisruptionException) { }
                finally
                {
                    hs.RemoveOverlay(overlay);
                }
            }
        }

        private void update()
        {
            overlay.UpdateStatus("Updating Cards...");
            board.UpdateCards();
            CardKeeper.FilterAndAssumeBelongings(board);
            card_keeper.CategorizeBoard(board);
            overlay.UpdateBoardInfo(card_keeper);

            var categorized_cards = card_keeper.CategorizedCards;

            var combo_stats = new Dictionary<string, double[]>();
            var deck_count = card_keeper.DeckCount;
            var all_cards = card_keeper.CategorizedCards;

            //Combos
            foreach (var combo in combos)
            {
                if (combo.Cards == null
                    || is_invalid_combo(combo.Cards, all_cards))
                    continue;

                int minimum_cards_count = combo.Cards.Count;
                int fitting_cards_count = 0;
                foreach(var card in combo.Cards.Distinct())
                {
                    fitting_cards_count += card.CardCount;
                }

                var rounds = new double[10];

                for(var i = minimum_cards_count; i < rounds.Length; ++i)
                {
                    rounds[i] = hypergeometric_distribution(deck_count, fitting_cards_count, i, minimum_cards_count);
                }

                combo_stats.Add(combo.Name, rounds);
            }

            overlay.UpdateComboStatistics(combo_stats);
        }

        private bool is_invalid_combo(List<ComboCard> combo_cards, IReadOnlyList<CategorizedCard> all_cards)
        {
            var card_count = new Dictionary<string, int>();
            
            /*
             * Combo is invalid if to many cards have been spent and is in the graveyard
             */

            foreach (var combo_card in combo_cards)
            {
                foreach (var categorized_card in all_cards)
                {
                    if (categorized_card.card.card_id == combo_card?.CardItem.cardId)
                    {
                        if (categorized_card.alliance == Alliance.friend && categorized_card.card.zone == MemoryZone.GRAVEYARD)
                        {
                            if (combo_card.CardCount <= 1)
                                return true;
                            else
                            {
                                int deck_card_count = 0;
                                if (card_count.TryGetValue(categorized_card.card.card_id, out deck_card_count))
                                {
                                    if (deck_card_count > 1)
                                    {
                                        --deck_card_count;
                                        card_count[categorized_card.card.card_id] = deck_card_count;
                                    }
                                    else
                                        return true;
                                }
                                else
                                {
                                    card_count[categorized_card.card.card_id] = combo_card.CardCount;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        private static double binominal_coefficient(double n, double k)
        {
            Debug.Assert(k <= n);
            if (k == 0 || n == k)
                return 1;
            else
                return (n / k) * binominal_coefficient(n - 1, k - 1);
        }

        private static double hypergeometric_distribution(double population_size, double success_states, double num_draws, double observed_successes)
        {
            return (binominal_coefficient(success_states, observed_successes) * binominal_coefficient(population_size - success_states, num_draws - observed_successes))
                / binominal_coefficient(population_size, num_draws);
        }

        void inc_data_trust()
        {
            if(disruption_count > 0)
            {
                --disruption_count;
                overlay.UpdateDisruption(disruption_count);
            }
        }

        void dec_data_trust()
        {
            ++disruption_count;
            overlay.UpdateDisruption(disruption_count);
            if (disruption_count > MAX_TOLERABLE_DISRUPTION)
                throw new DisruptionException();
        }

        bool our_turn(TrackDisruption track_dis)
        {
            var turn_button_text = read_turn_button(track_dis);
            if(turn_button_text.Contains("END"))
            {
                inc_data_trust();
                return true;
            }
            else
            {
                return false;
            }
        }

        void wait_until_our_turn(TrackDisruption track_dis)
        {
            for (;;)
            {
                if (our_turn(track_dis))
                    return;
                Thread.Sleep(10);
            }
        }

        void wait_for_match_start_frame()
        {
            for (;;)
            {
                if (is_start_game_frame())
                    return;
                Thread.Sleep(1000);
            }
        }

        void wait_for_match_start()
        {
            for (;;)
            {
                if (!is_start_game_frame())
                    return;
                Thread.Sleep(1000);
            }
        }

        private bool is_start_game_frame()
        {
            var text = hs.GetPixels()
                    .Slice(457.807953443259, 773.75, 120, 40)
                    .ReadText(250, 255, Color.White, Color.Black);
            return text.Contains("Confirm");
        }

        void wait_until_green_turn_button_or_enemy_turn()
        {
            for (;;)
            {
                var bitmap = get_turn_button_pixels();
                var turn_button_text = read_turn_button(bitmap, TrackDisruption.Yes);

                if (turn_button_text.Contains("ENEMY"))
                {
                    inc_data_trust();
                    return;
                }

                else if (turn_button_text.Contains("END"))
                {
                    inc_data_trust();
                    var green_index = bitmap.FindPixel(39, 208, 6);
                    if (green_index != null)
                        return;
                }

                Thread.Sleep(10);
            }
        }

        IEasyBitmap get_turn_button_pixels()
        {
            return hs.GetPixels()
                .Slice(861.299709020369, 461.25, 120, 30);
        }

        string read_turn_button(TrackDisruption track_dis)
        {
            return read_turn_button(get_turn_button_pixels(), track_dis);
        }

        string read_turn_button(IEasyBitmap bitmap, TrackDisruption track_disruption)
        {
            var result = bitmap
                .ReadText(0, 30, Color.White, Color.Black);
            if (track_disruption == TrackDisruption.Yes && string.IsNullOrWhiteSpace(result))
            {
                dec_data_trust();
                Thread.Sleep(500);
            }
            return result;
        }
    }

    internal class DisruptionException : Exception
    {
        public DisruptionException()
        {
        }
    }
}
