using BotPlugin.API;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ComboHelper
{
    [Serializable]
    public class CardJson
    {
        public string cardId;
        public string name;
        public string cardSet;
        public string type;
        public string faction;
        public string rarity;
        public int cost;
        public int attack;
        public int health;
        public string text;
        public string flavor;
        public string artist;
        public bool collectible;
        public bool elite;
        public string race;
        public string img;
        public string imgGold;
        public string locale;

        internal Image DownloadImage(WebClient web_client)
        {
            if (string.IsNullOrEmpty(img))
                return null;

            var file = Path.GetTempFileName();
            web_client.DownloadFile(img, file);
            return Image.FromFile(file);
        }

        internal static List<CardJson> GetCards(IHTTPClient client, string name)
        {
            name = WebUtility.UrlEncode(name);
            var url = "https://omgvamp-hearthstone-v1.p.mashape.com/cards/search/" + name;

            var request = new HTTPRequest(url);
            request.Headers.Add("X-Mashape-Key", "LwMznBxPOQmshqpFNAjXnDf2TlPZp1XmXL5jsnNmd2YoY1PqdV");
            request.Headers.Add(HttpRequestHeader.Accept, "application/json");

            return client.Get(request).JSON<List<CardJson>>();
        }
    }
}
