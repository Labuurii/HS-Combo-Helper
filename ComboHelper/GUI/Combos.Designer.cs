namespace ComboHelper.GUI
{
    partial class CombosWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CombosWindow));
            this.combosList = new System.Windows.Forms.ListView();
            this.combosMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addComboBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.removeComboBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.changeNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cardsList = new System.Windows.Forms.ListView();
            this.cardsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeCardBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.searchText = new System.Windows.Forms.TextBox();
            this.cardImg = new System.Windows.Forms.PictureBox();
            this.cardCountLbl = new System.Windows.Forms.Label();
            this.cardCountText = new System.Windows.Forms.TextBox();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.decksMainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.addDeckMI = new System.Windows.Forms.ToolStripMenuItem();
            this.comboLbl = new System.Windows.Forms.Label();
            this.cardsLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.deckCB = new System.Windows.Forms.ComboBox();
            this.deckIndexLbl = new System.Windows.Forms.Label();
            this.deckIndexText = new System.Windows.Forms.TextBox();
            this.combosMenu.SuspendLayout();
            this.cardsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardImg)).BeginInit();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // combosList
            // 
            this.combosList.ContextMenuStrip = this.combosMenu;
            this.combosList.LabelEdit = true;
            this.combosList.Location = new System.Drawing.Point(12, 120);
            this.combosList.MultiSelect = false;
            this.combosList.Name = "combosList";
            this.combosList.Size = new System.Drawing.Size(169, 223);
            this.combosList.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.combosList.TabIndex = 0;
            this.combosList.UseCompatibleStateImageBehavior = false;
            this.combosList.View = System.Windows.Forms.View.List;
            this.combosList.Visible = false;
            this.combosList.SelectedIndexChanged += new System.EventHandler(this.combosList_SelectedIndexChanged);
            // 
            // combosMenu
            // 
            this.combosMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addComboBtn,
            this.removeComboBtn,
            this.changeNameToolStripMenuItem});
            this.combosMenu.Name = "combosMenu";
            this.combosMenu.Size = new System.Drawing.Size(151, 70);
            // 
            // addComboBtn
            // 
            this.addComboBtn.Name = "addComboBtn";
            this.addComboBtn.Size = new System.Drawing.Size(150, 22);
            this.addComboBtn.Text = "Add";
            this.addComboBtn.Click += new System.EventHandler(this.addComboBtn_Click);
            // 
            // removeComboBtn
            // 
            this.removeComboBtn.Name = "removeComboBtn";
            this.removeComboBtn.Size = new System.Drawing.Size(150, 22);
            this.removeComboBtn.Text = "Remove";
            this.removeComboBtn.Click += new System.EventHandler(this.removeComboBtn_Click);
            // 
            // changeNameToolStripMenuItem
            // 
            this.changeNameToolStripMenuItem.Name = "changeNameToolStripMenuItem";
            this.changeNameToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.changeNameToolStripMenuItem.Text = "Change Name";
            this.changeNameToolStripMenuItem.Click += new System.EventHandler(this.changeNameToolStripMenuItem_Click);
            // 
            // cardsList
            // 
            this.cardsList.ContextMenuStrip = this.cardsMenu;
            this.cardsList.Location = new System.Drawing.Point(187, 120);
            this.cardsList.MultiSelect = false;
            this.cardsList.Name = "cardsList";
            this.cardsList.Size = new System.Drawing.Size(170, 223);
            this.cardsList.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.cardsList.TabIndex = 2;
            this.cardsList.UseCompatibleStateImageBehavior = false;
            this.cardsList.View = System.Windows.Forms.View.List;
            this.cardsList.Visible = false;
            this.cardsList.SelectedIndexChanged += new System.EventHandler(this.cardsList_SelectedIndexChanged);
            // 
            // cardsMenu
            // 
            this.cardsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeCardBtn});
            this.cardsMenu.Name = "cardsMenu";
            this.cardsMenu.Size = new System.Drawing.Size(118, 48);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // removeCardBtn
            // 
            this.removeCardBtn.Name = "removeCardBtn";
            this.removeCardBtn.Size = new System.Drawing.Size(117, 22);
            this.removeCardBtn.Text = "Remove";
            this.removeCardBtn.Click += new System.EventHandler(this.removeCardBtn_Click);
            // 
            // searchText
            // 
            this.searchText.Location = new System.Drawing.Point(366, 120);
            this.searchText.Name = "searchText";
            this.searchText.Size = new System.Drawing.Size(176, 20);
            this.searchText.TabIndex = 4;
            this.searchText.Text = "Enter card name";
            this.searchText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchText_KeyDown);
            // 
            // cardImg
            // 
            this.cardImg.Location = new System.Drawing.Point(366, 146);
            this.cardImg.Name = "cardImg";
            this.cardImg.Size = new System.Drawing.Size(176, 171);
            this.cardImg.TabIndex = 5;
            this.cardImg.TabStop = false;
            // 
            // cardCountLbl
            // 
            this.cardCountLbl.AutoSize = true;
            this.cardCountLbl.Location = new System.Drawing.Point(363, 326);
            this.cardCountLbl.Name = "cardCountLbl";
            this.cardCountLbl.Size = new System.Drawing.Size(59, 13);
            this.cardCountLbl.TabIndex = 6;
            this.cardCountLbl.Text = "Card count";
            this.cardCountLbl.Visible = false;
            // 
            // cardCountText
            // 
            this.cardCountText.Location = new System.Drawing.Point(436, 323);
            this.cardCountText.Name = "cardCountText";
            this.cardCountText.Size = new System.Drawing.Size(106, 20);
            this.cardCountText.TabIndex = 7;
            this.cardCountText.Text = "1";
            this.cardCountText.Visible = false;
            this.cardCountText.Validating += new System.ComponentModel.CancelEventHandler(this.cardCountText_Validating);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.decksMainMenu});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(554, 24);
            this.mainMenu.TabIndex = 9;
            this.mainMenu.Text = "menuStrip1";
            // 
            // decksMainMenu
            // 
            this.decksMainMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDeckMI});
            this.decksMainMenu.Name = "decksMainMenu";
            this.decksMainMenu.Size = new System.Drawing.Size(73, 20);
            this.decksMainMenu.Text = "Edit Decks";
            // 
            // addDeckMI
            // 
            this.addDeckMI.Name = "addDeckMI";
            this.addDeckMI.Size = new System.Drawing.Size(96, 22);
            this.addDeckMI.Text = "Add";
            this.addDeckMI.Click += new System.EventHandler(this.addDeckMI_Click);
            // 
            // comboLbl
            // 
            this.comboLbl.AutoSize = true;
            this.comboLbl.Location = new System.Drawing.Point(61, 87);
            this.comboLbl.Name = "comboLbl";
            this.comboLbl.Size = new System.Drawing.Size(45, 13);
            this.comboLbl.TabIndex = 10;
            this.comboLbl.Text = "Combos";
            this.comboLbl.Visible = false;
            // 
            // cardsLbl
            // 
            this.cardsLbl.AutoSize = true;
            this.cardsLbl.Location = new System.Drawing.Point(241, 87);
            this.cardsLbl.Name = "cardsLbl";
            this.cardsLbl.Size = new System.Drawing.Size(34, 13);
            this.cardsLbl.TabIndex = 11;
            this.cardsLbl.Text = "Cards";
            this.cardsLbl.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(419, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Info";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Current Deck";
            // 
            // deckCB
            // 
            this.deckCB.FormattingEnabled = true;
            this.deckCB.Location = new System.Drawing.Point(88, 45);
            this.deckCB.Name = "deckCB";
            this.deckCB.Size = new System.Drawing.Size(121, 21);
            this.deckCB.Sorted = true;
            this.deckCB.TabIndex = 14;
            this.deckCB.SelectedIndexChanged += new System.EventHandler(this.currentDeckCB_SelectedIndexChanged);
            // 
            // deckIndexLbl
            // 
            this.deckIndexLbl.AutoSize = true;
            this.deckIndexLbl.Location = new System.Drawing.Point(229, 48);
            this.deckIndexLbl.Name = "deckIndexLbl";
            this.deckIndexLbl.Size = new System.Drawing.Size(62, 13);
            this.deckIndexLbl.TabIndex = 15;
            this.deckIndexLbl.Text = "Deck Index";
            this.deckIndexLbl.Visible = false;
            // 
            // deckIndexText
            // 
            this.deckIndexText.Location = new System.Drawing.Point(297, 45);
            this.deckIndexText.Name = "deckIndexText";
            this.deckIndexText.Size = new System.Drawing.Size(100, 20);
            this.deckIndexText.TabIndex = 16;
            this.deckIndexText.Visible = false;
            this.deckIndexText.TextChanged += new System.EventHandler(this.deckIndexText_TextChanged);
            // 
            // CombosWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 355);
            this.Controls.Add(this.deckIndexText);
            this.Controls.Add(this.deckIndexLbl);
            this.Controls.Add(this.deckCB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cardsLbl);
            this.Controls.Add(this.comboLbl);
            this.Controls.Add(this.cardCountText);
            this.Controls.Add(this.cardCountLbl);
            this.Controls.Add(this.cardImg);
            this.Controls.Add(this.searchText);
            this.Controls.Add(this.cardsList);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.combosList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "CombosWindow";
            this.Text = "Combos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Combos_FormClosing);
            this.Load += new System.EventHandler(this.Combos_Load);
            this.combosMenu.ResumeLayout(false);
            this.cardsMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cardImg)).EndInit();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView combosList;
        private System.Windows.Forms.ContextMenuStrip combosMenu;
        private System.Windows.Forms.ToolStripMenuItem removeComboBtn;
        private System.Windows.Forms.ToolStripMenuItem changeNameToolStripMenuItem;
        private System.Windows.Forms.ListView cardsList;
        private System.Windows.Forms.ContextMenuStrip cardsMenu;
        private System.Windows.Forms.ToolStripMenuItem removeCardBtn;
        private System.Windows.Forms.TextBox searchText;
        private System.Windows.Forms.PictureBox cardImg;
        private System.Windows.Forms.Label cardCountLbl;
        private System.Windows.Forms.TextBox cardCountText;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem decksMainMenu;
        private System.Windows.Forms.ToolStripMenuItem addDeckMI;
        private System.Windows.Forms.Label comboLbl;
        private System.Windows.Forms.Label cardsLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox deckCB;
        private System.Windows.Forms.ToolStripMenuItem addComboBtn;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.Label deckIndexLbl;
        private System.Windows.Forms.TextBox deckIndexText;
    }
}