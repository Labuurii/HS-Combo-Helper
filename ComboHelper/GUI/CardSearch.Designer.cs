namespace ComboHelper.GUI
{
    partial class CardSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardSearch));
            this.selectBtn = new System.Windows.Forms.Button();
            this.searchText = new System.Windows.Forms.TextBox();
            this.cardsList = new System.Windows.Forms.ListView();
            this.cardImageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // selectBtn
            // 
            this.selectBtn.Location = new System.Drawing.Point(307, 216);
            this.selectBtn.Name = "selectBtn";
            this.selectBtn.Size = new System.Drawing.Size(75, 23);
            this.selectBtn.TabIndex = 1;
            this.selectBtn.Text = "Select";
            this.selectBtn.UseVisualStyleBackColor = true;
            this.selectBtn.Click += new System.EventHandler(this.selectBtn_Click);
            // 
            // searchText
            // 
            this.searchText.Location = new System.Drawing.Point(13, 12);
            this.searchText.Name = "searchText";
            this.searchText.Size = new System.Drawing.Size(100, 20);
            this.searchText.TabIndex = 2;
            this.searchText.Click += new System.EventHandler(this.searchText_Click);
            this.searchText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchText_KeyDown);
            // 
            // cardsList
            // 
            this.cardsList.LargeImageList = this.cardImageList;
            this.cardsList.Location = new System.Drawing.Point(13, 39);
            this.cardsList.Name = "cardsList";
            this.cardsList.Size = new System.Drawing.Size(369, 171);
            this.cardsList.SmallImageList = this.cardImageList;
            this.cardsList.TabIndex = 0;
            this.cardsList.UseCompatibleStateImageBehavior = false;
            // 
            // cardImageList
            // 
            this.cardImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.cardImageList.ImageSize = new System.Drawing.Size(128, 180);
            this.cardImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // CardSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 262);
            this.Controls.Add(this.searchText);
            this.Controls.Add(this.selectBtn);
            this.Controls.Add(this.cardsList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CardSearch";
            this.Text = "CardSearch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button selectBtn;
        private System.Windows.Forms.TextBox searchText;
        private System.Windows.Forms.ListView cardsList;
        private System.Windows.Forms.ImageList cardImageList;
    }
}