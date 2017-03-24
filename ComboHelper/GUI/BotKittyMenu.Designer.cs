namespace ComboHelper.GUI
{
    partial class BotKittyMenu
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.combosBtn = new System.Windows.Forms.Button();
            this.selectedDeckCB = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.combosBtn);
            this.flowLayoutPanel1.Controls.Add(this.selectedDeckCB);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(197, 281);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // combosBtn
            // 
            this.combosBtn.Location = new System.Drawing.Point(3, 3);
            this.combosBtn.Name = "combosBtn";
            this.combosBtn.Size = new System.Drawing.Size(75, 23);
            this.combosBtn.TabIndex = 0;
            this.combosBtn.Text = "Combos";
            this.combosBtn.UseVisualStyleBackColor = true;
            this.combosBtn.Click += new System.EventHandler(this.combosBtn_Click);
            // 
            // selectedDeckCB
            // 
            this.selectedDeckCB.FormattingEnabled = true;
            this.selectedDeckCB.Location = new System.Drawing.Point(3, 32);
            this.selectedDeckCB.Name = "selectedDeckCB";
            this.selectedDeckCB.Size = new System.Drawing.Size(121, 21);
            this.selectedDeckCB.TabIndex = 1;
            this.selectedDeckCB.Text = "Select Deck";
            this.selectedDeckCB.SelectedIndexChanged += new System.EventHandler(this.selectedDeckCB_SelectedIndexChanged);
            // 
            // BotKittyMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "BotKittyMenu";
            this.Size = new System.Drawing.Size(197, 284);
            this.Load += new System.EventHandler(this.BotKittyMenu_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button combosBtn;
        private System.Windows.Forms.ComboBox selectedDeckCB;
    }
}
