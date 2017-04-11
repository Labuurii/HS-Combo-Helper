using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BotPlugin.API;

namespace ComboHelper.GUI
{
    public partial class BotKittyMenu : UserControl
    {
        CombosWindow combos_menu;
        IDesktopAPI api;
        List<DeckItem> decks;

        internal BotKittyMenu(IDesktopAPI api, List<DeckItem> decks)
        {
            this.api = api;
            this.decks = decks;

            InitializeComponent();

            init_combos();
            decks_changed(decks);
        }

        private void BotKittyMenu_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            flowLayoutPanel1.Dock = DockStyle.Fill;
        }

        private void combosBtn_Click(object sender, EventArgs e)
        {
            if (combos_menu.IsDisposed)
                init_combos();
            combos_menu.Show();
        }

        private void init_combos()
        {
            combos_menu = new CombosWindow(api, decks);
            combos_menu.OnDecksChanged += decks_changed;
        }

        private void decks_changed(List<DeckItem> decks)
        {
            this.decks = decks;
            var items = selectedDeckCB.Items;
            items.Clear();
            foreach(var deck in decks)
                items.Add(deck);
        }

        private void selectedDeckCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = selectedDeckCB.SelectedItem as DeckItem;
            if (item == null)
                return;

            try
            {
                ComboStore.SelectDeck(item);
            } catch { }
        }
    }
}
