namespace ComboHelper.GUI
{
    partial class Overlay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.statusLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.disruptionLbl = new System.Windows.Forms.Label();
            this.allCardsTab = new System.Windows.Forms.TabPage();
            this.allCardsList = new System.Windows.Forms.ListBox();
            this.enemyTab = new System.Windows.Forms.TabPage();
            this.enemyCardsList = new System.Windows.Forms.ListBox();
            this.friendlyTab = new System.Windows.Forms.TabPage();
            this.friendlyCardsList = new System.Windows.Forms.ListBox();
            this.comboStats = new System.Windows.Forms.TabPage();
            this.combosList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.infoTabs = new System.Windows.Forms.TabControl();
            this.label3 = new System.Windows.Forms.Label();
            this.deckCountLbl = new System.Windows.Forms.Label();
            this.allCardsTab.SuspendLayout();
            this.enemyTab.SuspendLayout();
            this.friendlyTab.SuspendLayout();
            this.comboStats.SuspendLayout();
            this.infoTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Status";
            // 
            // statusLbl
            // 
            this.statusLbl.AutoSize = true;
            this.statusLbl.Location = new System.Drawing.Point(96, 12);
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(0, 13);
            this.statusLbl.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Disruption";
            // 
            // disruptionLbl
            // 
            this.disruptionLbl.AutoSize = true;
            this.disruptionLbl.Location = new System.Drawing.Point(96, 28);
            this.disruptionLbl.Name = "disruptionLbl";
            this.disruptionLbl.Size = new System.Drawing.Size(0, 13);
            this.disruptionLbl.TabIndex = 3;
            // 
            // allCardsTab
            // 
            this.allCardsTab.Controls.Add(this.allCardsList);
            this.allCardsTab.Location = new System.Drawing.Point(4, 22);
            this.allCardsTab.Name = "allCardsTab";
            this.allCardsTab.Padding = new System.Windows.Forms.Padding(3);
            this.allCardsTab.Size = new System.Drawing.Size(262, 201);
            this.allCardsTab.TabIndex = 0;
            this.allCardsTab.Text = "All Cards";
            this.allCardsTab.UseVisualStyleBackColor = true;
            // 
            // allCardsList
            // 
            this.allCardsList.FormattingEnabled = true;
            this.allCardsList.Location = new System.Drawing.Point(5, 7);
            this.allCardsList.Name = "allCardsList";
            this.allCardsList.Size = new System.Drawing.Size(254, 186);
            this.allCardsList.Sorted = true;
            this.allCardsList.TabIndex = 1;
            // 
            // enemyTab
            // 
            this.enemyTab.Controls.Add(this.enemyCardsList);
            this.enemyTab.Location = new System.Drawing.Point(4, 22);
            this.enemyTab.Name = "enemyTab";
            this.enemyTab.Size = new System.Drawing.Size(262, 201);
            this.enemyTab.TabIndex = 6;
            this.enemyTab.Text = "Enemies";
            this.enemyTab.UseVisualStyleBackColor = true;
            // 
            // enemyCardsList
            // 
            this.enemyCardsList.FormattingEnabled = true;
            this.enemyCardsList.Location = new System.Drawing.Point(3, 7);
            this.enemyCardsList.Name = "enemyCardsList";
            this.enemyCardsList.Size = new System.Drawing.Size(256, 186);
            this.enemyCardsList.Sorted = true;
            this.enemyCardsList.TabIndex = 3;
            // 
            // friendlyTab
            // 
            this.friendlyTab.Controls.Add(this.friendlyCardsList);
            this.friendlyTab.Location = new System.Drawing.Point(4, 22);
            this.friendlyTab.Name = "friendlyTab";
            this.friendlyTab.Size = new System.Drawing.Size(262, 201);
            this.friendlyTab.TabIndex = 5;
            this.friendlyTab.Text = "Friendly";
            this.friendlyTab.UseVisualStyleBackColor = true;
            // 
            // friendlyCardsList
            // 
            this.friendlyCardsList.FormattingEnabled = true;
            this.friendlyCardsList.Location = new System.Drawing.Point(3, 3);
            this.friendlyCardsList.Name = "friendlyCardsList";
            this.friendlyCardsList.Size = new System.Drawing.Size(259, 186);
            this.friendlyCardsList.Sorted = true;
            this.friendlyCardsList.TabIndex = 2;
            // 
            // comboStats
            // 
            this.comboStats.Controls.Add(this.combosList);
            this.comboStats.Location = new System.Drawing.Point(4, 22);
            this.comboStats.Name = "comboStats";
            this.comboStats.Size = new System.Drawing.Size(262, 201);
            this.comboStats.TabIndex = 4;
            this.comboStats.Text = "Combo Stats";
            this.comboStats.UseVisualStyleBackColor = true;
            // 
            // combosList
            // 
            this.combosList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.combosList.Location = new System.Drawing.Point(4, 4);
            this.combosList.Name = "combosList";
            this.combosList.Size = new System.Drawing.Size(255, 194);
            this.combosList.TabIndex = 0;
            this.combosList.UseCompatibleStateImageBehavior = false;
            this.combosList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Draw Count";
            this.columnHeader1.Width = 98;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Chance";
            // 
            // infoTabs
            // 
            this.infoTabs.Controls.Add(this.comboStats);
            this.infoTabs.Controls.Add(this.friendlyTab);
            this.infoTabs.Controls.Add(this.enemyTab);
            this.infoTabs.Controls.Add(this.allCardsTab);
            this.infoTabs.Location = new System.Drawing.Point(4, 57);
            this.infoTabs.Name = "infoTabs";
            this.infoTabs.SelectedIndex = 0;
            this.infoTabs.Size = new System.Drawing.Size(270, 227);
            this.infoTabs.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(171, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Deck count";
            // 
            // deckCountLbl
            // 
            this.deckCountLbl.AutoSize = true;
            this.deckCountLbl.Location = new System.Drawing.Point(234, 28);
            this.deckCountLbl.Name = "deckCountLbl";
            this.deckCountLbl.Size = new System.Drawing.Size(0, 13);
            this.deckCountLbl.TabIndex = 6;
            // 
            // Overlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.deckCountLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.infoTabs);
            this.Controls.Add(this.disruptionLbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statusLbl);
            this.Controls.Add(this.label1);
            this.Name = "Overlay";
            this.Size = new System.Drawing.Size(277, 287);
            this.allCardsTab.ResumeLayout(false);
            this.enemyTab.ResumeLayout(false);
            this.friendlyTab.ResumeLayout(false);
            this.comboStats.ResumeLayout(false);
            this.infoTabs.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label statusLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label disruptionLbl;
        private System.Windows.Forms.TabPage allCardsTab;
        private System.Windows.Forms.ListBox allCardsList;
        private System.Windows.Forms.TabPage enemyTab;
        private System.Windows.Forms.ListBox enemyCardsList;
        private System.Windows.Forms.TabPage friendlyTab;
        private System.Windows.Forms.ListBox friendlyCardsList;
        private System.Windows.Forms.TabPage comboStats;
        private System.Windows.Forms.TabControl infoTabs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label deckCountLbl;
        private System.Windows.Forms.ListView combosList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}
