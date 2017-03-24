using BotPlugin.API;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboHelper
{
    internal enum ActualZone
    {
        Unknown,
        Graveyard,
        Hand,
        EnemyPlay,
        FriendlyPlay
    }

    internal enum MemoryZone
    {
        HAND,
        PLAY,
        DECK,
        GRAVEYARD,
        Unknown
    }

    internal class MemoryCard
    {
        internal int game_id;
        internal string card_id;
        internal string name;
        internal MemoryZone zone;
        internal int zone_pos;

        internal MemoryCard()
        {
            game_id = -1;
            card_id = null;
            name = null;
            zone = MemoryZone.Unknown;
            zone_pos = 0;
        }

        public override bool Equals(object obj)
        {
            var mem_card = obj as MemoryCard;
            if (mem_card != null)
                return mem_card.game_id == game_id;

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return game_id.GetHashCode();
        }

        public override string ToString()
        {
            return name + "[game_id: " + game_id.ToString() + " card_id: " + card_id + " zone: " + zone.ToString() + " zone_pos: " + zone_pos.ToString();
        }
    }

    internal class Board
    {
        IProcess hs;

        List<MemoryCard> cards = new List<MemoryCard>();

        internal Board(IProcess hs)
        {
            this.hs = hs;
        }

        internal List<MemoryCard> Cards
        {
            get
            {
                return cards;
            }
        }

        internal void UpdateCards()
        {
            cards.Clear();

            var possible_cards = hs.MemFind("zone=");
            var processed_cards = possible_cards
                .AsParallel()
                .Select(delegate (IntPtr address)
                {
                    var card = new MemoryCard();
                    try
                    {
                        var card_string = hs.MemReadString(address);
                        var parts = card_string.Split('[');
                        

                        if (parts.Length == 3 && parts[2].EndsWith("]"))
                        {
                            parts = parts[2].Split(' ');
                            if (parts.Length != 3)
                                return card;
                            card.game_id = key_value_int("id", parts[0]);
                            var zone = key_value_zone(parts[1]);
                            if (zone != MemoryZone.DECK)
                                return card;
                            card.zone = zone;
                            card.zone_pos = key_value_int("zonePos", parts[2]);
                            return card;
                        }

                        if (parts.Length != 2)
                            return card;


                        card.name = parts[0].Trim();

                        parts = parts[1].Split(' ');
                        if (parts.Length != 4)
                            return card;
                        card.game_id = key_value_int("id", parts[0]);
                        card.card_id = key_value_string("cardId", parts[1]);
                        card.zone = key_value_zone(parts[2]);
                        card.zone_pos = key_value_int("zonePos", parts[3]);
                    }
                    catch { }

                    return card;
                })
                .Where(delegate (MemoryCard card)
                {
                    return card.game_id != -1
                        && card.zone != MemoryZone.Unknown
                        && card.zone_pos != -1
                        && !(card.zone != MemoryZone.DECK && string.IsNullOrWhiteSpace(card.name));
                });

            cards.AddRange(processed_cards);
        }

        private static string key_value_string(string expected_key, string str)
        {
            var kv = str.Split('=');
            if (kv.Length != 2)
                return null;
            var key = kv[0].TrimStart('[', ' ');
            if (key != expected_key)
                return null;
            return kv[1].TrimEnd(']', ' ');
        }

        private static MemoryZone key_value_zone(string str)
        {
            var result = key_value_string("zone", str);
            MemoryZone zone;
            if (!Enum.TryParse(result, out zone))
                return MemoryZone.Unknown;
            return zone;
        }

        private static int key_value_int(string expected_key, string str)
        {
            var result = key_value_string(expected_key, str);
            if (result == null)
                return -1;

            int int_value;
            if (!int.TryParse(result, out int_value))
                return -1;
            return int_value;
        }
    }
}
