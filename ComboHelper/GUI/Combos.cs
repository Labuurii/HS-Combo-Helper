using BotPlugin.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComboHelper.GUI
{
    internal delegate void DecksChangedHandler(List<DeckItem> decks);

    public partial class CombosWindow : Form
    {
        CardSearch card_search;
        WebClient web_client = new WebClient();
        List<DeckItem> decks;

        internal event DecksChangedHandler OnDecksChanged;

        internal CombosWindow(IDesktopAPI api, List<DeckItem> decks)
        {
            card_search = new CardSearch(api.HttpClient);
            this.decks = decks;

            InitializeComponent();

            foreach (var deck in decks)
                display_deck(deck);

            card_search.OnCardsSelected += new_cards_selected;
        }

        private void new_cards_selected(ListView.SelectedListViewItemCollection card_items)
        {
            var current_combo = get_current_combo();
            if (current_combo == null)
                return;

            if (current_combo.Cards == null)
                current_combo.Cards = new List<ComboCard>();

            if(card_items.Count > 0)
            {
                cardCountLbl.Visible = true;
                cardCountText.Visible = true;
            }

            foreach(CardItem card_item in card_items)
            {
                var combo_card = new ComboCard(card_item);
                current_combo.Cards.Add(combo_card);

                var item = new ListViewItem(combo_card.ToString());
                item.Tag = combo_card;
                cardsList.Items.Add(item);
            }
        }

        private void search_for_card()
        {
            var name = searchText.Text;
            if (string.IsNullOrEmpty(name))
                return;

            card_search.Search(name);
            card_search.ShowDialog();
        }

        private void verify_card_count()
        {
            int gibberish_int;
            if(!int.TryParse(cardCountText.Text, out gibberish_int))
            {
                MessageBox.Show("Card count is not a number. Card will not be saved as it is now.", "Error");
            }
        }

        private void add_deck(string name)
        {
            var deck = new DeckItem(name);
            add_deck(deck);
        }

        private void add_deck(DeckItem deck)
        {
            display_deck(deck);
            decks.Add(deck);
            OnDecksChanged?.Invoke(decks);
        }

        private void display_deck(DeckItem deck)
        {
            var textbox = new ToolStripTextBox();
            textbox.Size = new Size(152, 23);
            textbox.Text = deck.Name;

            textbox.TextChanged += update_deck_name;

            decksMainMenu.DropDownItems.Add(textbox);
            deckCB.Items.Add(deck);
        }

        private void update_deck_name(object sender, EventArgs e)
        {
            var textbox = sender as ToolStripTextBox;
            if(textbox != null)
            {
                var index = decksMainMenu.DropDownItems.IndexOf(textbox);
                Debug.Assert(index > 0);

                var cb_index = index - 1;

                
                var deck_item = (DeckItem) deckCB.Items[cb_index];
                deck_item.Name = textbox.Text;
                OnDecksChanged?.Invoke(decks);
                deckCB.Items[cb_index] = deck_item;
            }
        }

        private void change_deck_name(int index, string name)
        {
            var item = (DeckItem)deckCB.Items[index];
            item.Name = name;
        }

        private void remove_deck(int index)
        {
            var item = decksMainMenu.DropDownItems[index];
            item.TextChanged -= update_deck_name;
            deckCB.Items.RemoveAt(index);
        }

        private DeckItem get_deck(int index)
        {
            return (DeckItem) deckCB.Items[index];
        }

        private DeckItem get_current_deck()
        {
            var index = deckCB.SelectedIndex;
            if (index == -1)
                return null;
            return get_deck(index);
        }

        private ComboCard get_current_card()
        {
            var indexes = cardsList.SelectedIndices;
            if (indexes.Count != 1)
                return null;
            return (ComboCard) cardsList.Items[indexes[0]].Tag;
        }

        private void Combos_Load(object sender, EventArgs e)
        {

        }

        private void addDeckMI_Click(object sender, EventArgs e)
        {
            add_deck("Change Name");
        }

        private void currentDeckCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected_index = deckCB.SelectedIndex;
            if (selected_index == -1)
                return;

            var deck = get_deck(selected_index);
            comboLbl.Visible = true;
            combosList.Visible = true;

            combosList.Clear();
            if(deck.Combos != null)
            {
                foreach (var combo in deck.Combos)
                {
                    var item = new ListViewItem(combo.ToString());
                    item.Tag = combo;

                    combosList.Items.Add(item);
                }
            }
        }

        private void addComboBtn_Click(object sender, EventArgs e)
        {
            var deck = get_current_deck();
            if (deck == null) return;

            var combo = new Combo();
            if (deck.Combos == null)
                deck.Combos = new List<Combo>();
            deck.Combos.Add(combo);

            var item = new ListViewItem(combo.ToString());
            item.Tag = combo;
            combosList.Items.Add(item);
            item.BeginEdit();
        }

        private void combosList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combo_objs = combosList.SelectedItems;
            if (combo_objs.Count != 1)
            {
                cardsList.Visible = false;
                return;
            }

            cardsList.Visible = true;
            cardsLbl.Visible = true;
            sync_combo_names();
            cardsList.Clear();

            var combo = (Combo)combo_objs[0].Tag;
            if(combo.Cards != null)
            {
                foreach (var card in combo.Cards)
                {
                    var item = new ListViewItem(card.ToString());
                    item.Tag = card;
                    cardsList.Items.Add(item);
                }
            }
        }

        private void searchText_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                search_for_card();
            }
        }

        private void cardsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var card = get_current_card();
            if (card == null)
                return;


            if(card.CardImage == null)
            {
                try
                {
                    var img = card.CardItem.DownloadImage(web_client);
                    if (img != null)
                    {
                        cardImg.Image = new Bitmap(img, cardImg.Size);
                        card.CardImage = img;
                    }
                    else
                    {
                        cardImg.Image = cardImg.ErrorImage;
                    }
                    cardCountLbl.Visible
                        = cardCountText.Visible
                        = true;
                }
                catch(Exception) {
                    cardImg.Image = cardImg.ErrorImage;
                }
            }
            else
            {
                cardImg.Image = new Bitmap(card.CardImage, cardImg.Size);
            }
            cardCountText.Text = card.CardCount.ToString();
        }

        private void cardCountText_Validating(object sender, CancelEventArgs e)
        {
            verify_card_count();
        }

        private void Combos_FormClosing(object sender, FormClosingEventArgs form_closing)
        {
            try
            {
                sync_combo_names();

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
            catch(Exception)
            {
                var answer = MessageBox.Show("Could not save settings.\nD you which to continue anyway?\nNotice this means your settings will be lost.", "Error", MessageBoxButtons.YesNo);
                form_closing.Cancel = (answer == DialogResult.Yes);
            }
        }

        private void sync_combo_names()
        {
            foreach(ListViewItem obj in combosList.Items)
            {
                var combo = (Combo)obj.Tag;
                combo.Name = obj.Text;
            }
        }

        private void removeComboBtn_Click(object sender, EventArgs e)
        {
            var sel_objs = combosList.SelectedItems;
            if (sel_objs.Count != 1)
                return;

            var deck = get_current_deck();
            if (deck == null)
                return;

            var combo = (Combo)sel_objs[0].Tag;
            combosList.Items.Remove(sel_objs[0]);
            Debug.Assert(deck.Combos.Remove(combo));
        }

        private void changeNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sel_indices = combosList.SelectedIndices;
            if (sel_indices.Count == 1)
                combosList.Items[sel_indices[0]].BeginEdit();
        }

        private void addCardBtn_Click(object sender, EventArgs e)
        {
            card_search.ShowDialog();
        }

        private Combo get_current_combo()
        {
            var sel_objs = combosList.SelectedItems;
            if (sel_objs.Count == 1)
                return (Combo) sel_objs[0].Tag;
            return null;
        }

        private void removeCardBtn_Click(object sender, EventArgs e)
        {
            var combo = get_current_combo();
            if (combo == null)
                return;

            if (combo.Cards == null)
                return;

            var sel_objs = cardsList.SelectedIndices;
            if (sel_objs.Count != 1)
                return;

            combo.Cards.RemoveAt(sel_objs[0]);
            cardsList.Items.RemoveAt(sel_objs[0]);
        }

        private void addCardBtn_Click_1(object sender, EventArgs e)
        {
            card_search.ShowDialog();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            card_search.ShowDialog();
        }
    }

    [Serializable]
    internal class ComboCard
    {
        internal CardJson CardItem;
        internal int CardCount = 1;

        [NonSerialized]
        internal Image CardImage;

        public ComboCard() { }

        public ComboCard(CardItem card_item)
        {
            CardItem = card_item.card;
            CardImage = card_item.image;
        }

        public override int GetHashCode()
        {
            if (CardItem != null)
                return CardItem.cardId.GetHashCode();
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var rhs = obj as ComboCard;
            if (rhs != null)
            {
                return CardItem?.cardId == rhs?.CardItem.cardId;
            }
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return CardItem?.name;
        }
    }

    [Serializable]
    internal class Combo
    {
        internal string Name;
        internal List<ComboCard> Cards;

        public Combo() { }

        public override string ToString()
        {
            return Name;
        }
    }

    [Serializable]
    internal class DeckItem
    {
        internal string Name = "";
        internal int DeckIndex;
        internal List<Combo> Combos;

        public DeckItem(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
