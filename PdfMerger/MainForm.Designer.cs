namespace PdfMerger
{
    partial class MainForm
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
            this.InfoListBox = new System.Windows.Forms.ListBox();
            this.CreateTocButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.ConvertButton = new System.Windows.Forms.Button();
            this.InfoTextBox = new System.Windows.Forms.TextBox();
            this.FrTocButton = new System.Windows.Forms.Button();
            this.FrTovPdfButton = new System.Windows.Forms.Button();
            this.SelectDirButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InfoListBox
            // 
            this.InfoListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoListBox.FormattingEnabled = true;
            this.InfoListBox.Location = new System.Drawing.Point(124, 51);
            this.InfoListBox.Name = "InfoListBox";
            this.InfoListBox.Size = new System.Drawing.Size(794, 342);
            this.InfoListBox.TabIndex = 0;
            // 
            // CreateTocButton
            // 
            this.CreateTocButton.Location = new System.Drawing.Point(12, 12);
            this.CreateTocButton.Name = "CreateTocButton";
            this.CreateTocButton.Size = new System.Drawing.Size(106, 23);
            this.CreateTocButton.TabIndex = 2;
            this.CreateTocButton.Text = "Create TOC";
            this.CreateTocButton.UseVisualStyleBackColor = true;
            this.CreateTocButton.Click += new System.EventHandler(this.CreateTocButtonClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 212);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "ComposeButton";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ConvertButton
            // 
            this.ConvertButton.Location = new System.Drawing.Point(12, 41);
            this.ConvertButton.Name = "ConvertButton";
            this.ConvertButton.Size = new System.Drawing.Size(106, 23);
            this.ConvertButton.TabIndex = 4;
            this.ConvertButton.Text = "docx->pdf";
            this.ConvertButton.UseVisualStyleBackColor = true;
            this.ConvertButton.Click += new System.EventHandler(this.ConvertButtonClick);
            // 
            // InfoTextBox
            // 
            this.InfoTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoTextBox.Location = new System.Drawing.Point(124, 15);
            this.InfoTextBox.Name = "InfoTextBox";
            this.InfoTextBox.Size = new System.Drawing.Size(754, 20);
            this.InfoTextBox.TabIndex = 8;
            // 
            // FrTocButton
            // 
            this.FrTocButton.Location = new System.Drawing.Point(12, 154);
            this.FrTocButton.Name = "FrTocButton";
            this.FrTocButton.Size = new System.Drawing.Size(106, 23);
            this.FrTocButton.TabIndex = 9;
            this.FrTocButton.Text = "FrToc";
            this.FrTocButton.UseVisualStyleBackColor = true;
            this.FrTocButton.Click += new System.EventHandler(this.FrTocButton_Click);
            // 
            // FrTovPdfButton
            // 
            this.FrTovPdfButton.Location = new System.Drawing.Point(12, 183);
            this.FrTovPdfButton.Name = "FrTovPdfButton";
            this.FrTovPdfButton.Size = new System.Drawing.Size(106, 23);
            this.FrTovPdfButton.TabIndex = 10;
            this.FrTovPdfButton.Text = "FrTovPdf";
            this.FrTovPdfButton.UseVisualStyleBackColor = true;
            this.FrTovPdfButton.Click += new System.EventHandler(this.FrTovPdfButtonClick);
            // 
            // SelectDirButton
            // 
            this.SelectDirButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectDirButton.Location = new System.Drawing.Point(884, 12);
            this.SelectDirButton.Name = "SelectDirButton";
            this.SelectDirButton.Size = new System.Drawing.Size(34, 23);
            this.SelectDirButton.TabIndex = 11;
            this.SelectDirButton.Text = "...";
            this.SelectDirButton.UseVisualStyleBackColor = true;
            this.SelectDirButton.Click += new System.EventHandler(this.SelectDirButtonClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 411);
            this.Controls.Add(this.SelectDirButton);
            this.Controls.Add(this.FrTovPdfButton);
            this.Controls.Add(this.FrTocButton);
            this.Controls.Add(this.InfoTextBox);
            this.Controls.Add(this.ConvertButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CreateTocButton);
            this.Controls.Add(this.InfoListBox);
            this.Name = "MainForm";
            this.Text = "PdfMerger";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox InfoListBox;
        private System.Windows.Forms.Button CreateTocButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button ConvertButton;
        private System.Windows.Forms.TextBox InfoTextBox;
        private System.Windows.Forms.Button FrTocButton;
        private System.Windows.Forms.Button FrTovPdfButton;
        private System.Windows.Forms.Button SelectDirButton;
    }
}

