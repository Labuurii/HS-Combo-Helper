using BotPlugin.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComboHelper.GUI
{
    internal delegate void CardsSelectedHandler(ListView.SelectedListViewItemCollection card_items);

    public partial class CardSearch : Form
    {
        IHTTPClient http_client;
        WebClient web_client = new WebClient();

        internal event CardsSelectedHandler OnCardsSelected;

        public CardSearch(IHTTPClient http_client)
        {
            this.http_client = http_client;

            InitializeComponent();
        }

        internal void Search(string name)
        {
            searchText.Text = name;
            do_search(name);
        }

        private void do_search(string text)
        {
            Task.Run(delegate ()
            {
                try
                {
                    var cards = CardJson.GetCards(http_client, text);

                    Invoke((MethodInvoker)delegate ()
                   {
                       cardsList.Clear();
                       foreach (var card in cards)
                       {
                           var img = card.img;
                           if (!string.IsNullOrEmpty(img))
                           {
                               try
                               {
                                   var file = Path.GetTempFileName();
                                   web_client.DownloadFile(img, file);
                                   var image = Image.FromFile(file);

                                   cardImageList.Images.Add(image);
                                   var item = new CardItem(card, image);
                                   item.ImageIndex = cardsList.Items.Count;
                                   cardsList.Items.Add(item);
                               }
                               catch { }
                           }
                       }

                       for (var i = 0; i < cardImageList.Images.Count; ++i)
                       {
                           var item = new ListViewItem();
                           item.ImageIndex = i;
                           cardsList.Items.Add(item);
                       }
                   });
                }
                catch (Exception e)
                {
                    Invoke((MethodInvoker)delegate ()
                   {
                       MessageBox.Show("Could not find card with name " + text + " because:\n" + e.Message, "Error");
                   });
                }
            });
        }

        private void searchText_Validating(object sender, CancelEventArgs e)
        {

        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            var selected_items = cardsList.SelectedItems;
            if (selected_items.Count > 0)
            {
                OnCardsSelected?.Invoke(selected_items);
                Hide();
            }
        }

        private void searchText_Click(object sender, EventArgs e)
        {

        }

        private void searchText_KeyDown(object sender, KeyEventArgs e)
        {
            var text = searchText.Text;
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(text))
            {
                e.Handled = true;
                Search(text);
            }
        }
    }

    internal class CardItem : ListViewItem
    {
        internal CardJson card;
        internal Image image;

        public CardItem(CardJson card, Image image)
        {
            this.card = card;
            this.image = image;
        }
    }
}
