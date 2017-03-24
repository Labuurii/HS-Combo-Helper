using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComboHelper.GUI
{
    public partial class Overlay : UserControl
    {
        public Overlay()
        {
            InitializeComponent();
        }

        internal void UpdateStatus(string status)
        {
            Invoke((MethodInvoker)delegate ()
           {
               statusLbl.Text = status;
           });
        }

        internal void UpdateDisruption(int level)
        {
            Invoke((MethodInvoker)delegate ()
           {
               disruptionLbl.Text = level.ToString();
           });
        }

        internal void UpdateBoardInfo(CardKeeper card_keeper)
        {
            var deck_count = card_keeper.DeckCount;

            var friendly_cards = card_keeper.CategorizedCards.Where(
                delegate (CategorizedCard card)
                {
                    return card.alliance == Alliance.friend;
                })
                .Select(delegate (CategorizedCard c)
                {
                    return c.card;
                });

            var enemy_cards = card_keeper.CategorizedCards.Where(
                delegate (CategorizedCard card)
                {
                    return card.alliance == Alliance.enemy;
                })
                .Select(delegate(CategorizedCard c)
                {
                    return c.card;
                });

            var cards = card_keeper.CategorizedCards;

            Invoke((MethodInvoker)delegate ()
           {
               deckCountLbl.Text = deck_count.ToString();

               UpdateTabListInfo(allCardsList, cards);
               UpdateTabListInfo(friendlyCardsList, friendly_cards);
               UpdateTabListInfo(enemyCardsList, enemy_cards);
           });
        }

        private static void UpdateTabListInfo<T>(ListBox list, IEnumerable<T> cards)
        {
            var items = list.Items;
            items.Clear();
            foreach (var card in cards)
                items.Add(card);
        }

        private static void UpdateTabListInfo(ListBox list, IReadOnlyCollection<CategorizedCard> cards)
        {
            var items = list.Items;
            items.Clear();
            foreach (var card in cards)
                items.Add(card);
        }

        internal void UpdateComboStatistics(Dictionary<string, double[]> combo_stats)
        {
            Invoke((MethodInvoker)delegate ()
           {
               combosList.Items.Clear();
               combosList.Groups.Clear();
               foreach(var combo in combo_stats)
               {
                   var name = combo.Key;
                   var stats = combo.Value;

                   var group = new ListViewGroup(name);
                   combosList.Groups.Add(group);
                   for (var i = 0; i < stats.Length; ++i)
                   {
                       var stat = stats[i];
                       if (stat == 0)
                           continue;

                       var item = new ListViewItem(new string[] {
                           (i + 1).ToString(),
                           Math.Round(stat * 100, 3).ToString() + "%"
                       });

                       item.Group = group;
                       combosList.Items.Add(item);
                   }
               }
           });
        }
    }
}
