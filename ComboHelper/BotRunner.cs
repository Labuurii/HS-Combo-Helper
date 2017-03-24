using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotPlugin.API;
using System.Windows.Forms;
using ComboHelper.GUI;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Diagnostics;

namespace ComboHelper
{
    //TODO: Move this into it's own function, CreateBinder
    sealed class AllowAllAssemblyVersionsDeserializationBinder : System.Runtime.Serialization.SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type typeToDeserialize = null;

            string currentAssembly = Assembly.GetExecutingAssembly().FullName;

            if (assemblyName.StartsWith(Path.GetFileNameWithoutExtension(currentAssembly)))
            {
                typeToDeserialize = Type.GetType(string.Format("{0}, {1}",
                        typeName, assemblyName));
            }
            else
            {
                typeToDeserialize = Type.GetType(typeName);
            }

            return typeToDeserialize;
        }
    }

    public class BotRunner : BotPlugin.BotRunner
    {
        BotKittyMenu menu;

        public override string GetName()
        {
            return "HS Combo Helper";
        }

        public override string GetAuthor()
        {
            return "Kickupx";
        }

        public override string GetDescription()
        {
            return "An absolutely fundamentally awesome bot. #42";
        }

        public override string GetVersion()
        {
            return "0.0.1";
        }

        public override void SetupSettingsGUI(IAPI iapi, Panel botPanel)
        {
            if(menu == null)
            {
                List<DeckItem> decks = null;
                try
                {

                    var ser_str = Properties.HSComboHelper.Default.decks;
                    var bytes = Convert.FromBase64String(ser_str);
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Binder = new AllowAllAssemblyVersionsDeserializationBinder();
                        var obj = bf.Deserialize(ms);
                        decks = (List<DeckItem>)obj;
                    }
                }
                catch { }

                if (decks == null)
                    decks = new List<DeckItem>();

                var api = (IDesktopAPI)iapi;
                menu = new BotKittyMenu(api, decks);
            }
            botPanel.Controls.Add(menu);
        }

        public override void OnStart(IAPI iapi)
        {
            Debug.Assert(menu != null);

            var selected_deck = menu.SelectedDeck;
            if (selected_deck == null)
                throw new Exception("You need to specify the deck you are using");            
            var api = (IDesktopAPI)iapi;
        
            var processes = api.Processes.ByName("Hearthstone");
            if (processes.Count > 1)
                throw new Exception("There is more than one botkitty instance running");
            else if (processes.Count == 0)
                throw new Exception("There is not any Hearthstone instance running");

            var hs = processes[0];
            var hs_window = hs.MainWindow;
            hs_window.Focus();

            api.Logger.LogNormal("Resizing window. Make sure to keep the window with this size");
            hs_window.SetRect(new Rectangle(0, 0, 1000, 800));

            var board = new Board(hs);

            //var combos = new List<Combo>();
            var combos = selected_deck.Combos;

            //var overlay = new Overlay();
            //hs_window.AddOverlay(500, 500, overlay);
            
            /*
            var stats = new Dictionary<string, double[]>();
            stats.Add("C1", new double[] { 1, 2, 3 });
            stats.Add("C2", new double[] { 1, 2, 3 });
            overlay.UpdateComboStatistics(stats);

            while (true) { Thread.Sleep(1); }
            */

            var bot = new GameKeeper(hs_window, board, combos);
            bot.bot_loop();
        }
    }
}
