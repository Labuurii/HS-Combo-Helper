using ComboHelper.GUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ComboHelper
{
    internal static class ComboStore
    {
        internal static List<DeckItem> Load()
        {
            try
            {

                var ser_str = Properties.HSComboHelper.Default.decks;
                var bytes = Convert.FromBase64String(ser_str);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Binder = new AllowAllAssemblyVersionsDeserializationBinder();
                    var obj = bf.Deserialize(ms);
                    return (List<DeckItem>)obj;
                }
            }
            catch { }

            return new List<DeckItem>();
        }

        internal static void Save(List<DeckItem> decks)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, decks);
                ms.Position = 0;
                byte[] buffer = new byte[(int)ms.Length];
                ms.Read(buffer, 0, buffer.Length);
                Properties.HSComboHelper.Default.decks = Convert.ToBase64String(buffer);
                Properties.HSComboHelper.Default.Save();
            }
        }

        internal static DeckItem SelectedDeck(List<DeckItem> decks)
        {
            var deck_name = Properties.HSComboHelper.Default.selected_deck;
            if (string.IsNullOrWhiteSpace(deck_name))
                return null;

            foreach(var deck in decks)
            {
                if (deck_name == deck.Name)
                    return deck;
            }

            return null;
        }

        internal static void SelectDeck(DeckItem deck)
        {
            Properties.HSComboHelper.Default.selected_deck = deck.Name;
            Properties.HSComboHelper.Default.Save();
        }
    }
}
