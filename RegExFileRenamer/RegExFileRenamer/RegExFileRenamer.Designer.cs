namespace RegExFileRenamer
{
    partial class RegExFileRenamer
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
            this.DirectoryTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OpenExplorerButton = new System.Windows.Forms.Button();
            this.ScanDirectoryButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.RegexTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TestRegexButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ReplacementTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.RegexTestedCheckBox = new System.Windows.Forms.CheckBox();
            this.FolderScannedCheckBox = new System.Windows.Forms.CheckBox();
            this.ApplyRegexButton = new System.Windows.Forms.Button();
            this.PostRegexListBox = new System.Windows.Forms.ListBox();
            this.FilesFoundListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // DirectoryTextBox
            // 
            this.DirectoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DirectoryTextBox.Location = new System.Drawing.Point(9, 26);
            this.DirectoryTextBox.Name = "DirectoryTextBox";
            this.DirectoryTextBox.Size = new System.Drawing.Size(678, 20);
            this.DirectoryTextBox.TabIndex = 0;
            this.DirectoryTextBox.TextChanged += new System.EventHandler(this.DirectoryTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "File Directory";
            // 
            // OpenExplorerButton
            // 
            this.OpenExplorerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenExplorerButton.Location = new System.Drawing.Point(693, 24);
            this.OpenExplorerButton.Name = "OpenExplorerButton";
            this.OpenExplorerButton.Size = new System.Drawing.Size(91, 23);
            this.OpenExplorerButton.TabIndex = 2;
            this.OpenExplorerButton.Text = "Open Explorer";
            this.OpenExplorerButton.UseVisualStyleBackColor = true;
            this.OpenExplorerButton.Click += new System.EventHandler(this.OpenExplorerButton_Click);
            // 
            // ScanDirectoryButton
            // 
            this.ScanDirectoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanDirectoryButton.Location = new System.Drawing.Point(790, 24);
            this.ScanDirectoryButton.Name = "ScanDirectoryButton";
            this.ScanDirectoryButton.Size = new System.Drawing.Size(91, 23);
            this.ScanDirectoryButton.TabIndex = 3;
            this.ScanDirectoryButton.Text = "Scan Directory";
            this.ScanDirectoryButton.UseVisualStyleBackColor = true;
            this.ScanDirectoryButton.Click += new System.EventHandler(this.ScanDirectoryButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Files In Directory";
            // 
            // RegexTextBox
            // 
            this.RegexTextBox.Location = new System.Drawing.Point(9, 65);
            this.RegexTextBox.Name = "RegexTextBox";
            this.RegexTextBox.Size = new System.Drawing.Size(401, 20);
            this.RegexTextBox.TabIndex = 7;
            this.RegexTextBox.TextChanged += new System.EventHandler(this.RegexTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Regular Expression";
            // 
            // TestRegexButton
            // 
            this.TestRegexButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TestRegexButton.Location = new System.Drawing.Point(790, 63);
            this.TestRegexButton.Name = "TestRegexButton";
            this.TestRegexButton.Size = new System.Drawing.Size(91, 23);
            this.TestRegexButton.TabIndex = 9;
            this.TestRegexButton.Text = "Test Regex";
            this.TestRegexButton.UseVisualStyleBackColor = true;
            this.TestRegexButton.Click += new System.EventHandler(this.TestRegexButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(413, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Replacement";
            // 
            // ReplacementTextBox
            // 
            this.ReplacementTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReplacementTextBox.Location = new System.Drawing.Point(416, 65);
            this.ReplacementTextBox.Name = "ReplacementTextBox";
            this.ReplacementTextBox.Size = new System.Drawing.Size(368, 20);
            this.ReplacementTextBox.TabIndex = 10;
            this.ReplacementTextBox.TextChanged += new System.EventHandler(this.ReplacementTextBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(413, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Files After Regex";
            // 
            // RegexTestedCheckBox
            // 
            this.RegexTestedCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RegexTestedCheckBox.AutoCheck = false;
            this.RegexTestedCheckBox.AutoSize = true;
            this.RegexTestedCheckBox.Location = new System.Drawing.Point(792, 91);
            this.RegexTestedCheckBox.Name = "RegexTestedCheckBox";
            this.RegexTestedCheckBox.Size = new System.Drawing.Size(93, 17);
            this.RegexTestedCheckBox.TabIndex = 14;
            this.RegexTestedCheckBox.Text = "Regex Tested";
            this.RegexTestedCheckBox.UseVisualStyleBackColor = true;
            // 
            // FolderScannedCheckBox
            // 
            this.FolderScannedCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FolderScannedCheckBox.AutoCheck = false;
            this.FolderScannedCheckBox.AutoSize = true;
            this.FolderScannedCheckBox.Location = new System.Drawing.Point(687, 91);
            this.FolderScannedCheckBox.Name = "FolderScannedCheckBox";
            this.FolderScannedCheckBox.Size = new System.Drawing.Size(101, 17);
            this.FolderScannedCheckBox.TabIndex = 16;
            this.FolderScannedCheckBox.Text = "Folder Scanned";
            this.FolderScannedCheckBox.UseVisualStyleBackColor = true;
            // 
            // ApplyRegexButton
            // 
            this.ApplyRegexButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ApplyRegexButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApplyRegexButton.Location = new System.Drawing.Point(9, 617);
            this.ApplyRegexButton.Name = "ApplyRegexButton";
            this.ApplyRegexButton.Size = new System.Drawing.Size(267, 27);
            this.ApplyRegexButton.TabIndex = 17;
            this.ApplyRegexButton.Text = "Apply Regex Rename";
            this.ApplyRegexButton.UseVisualStyleBackColor = true;
            this.ApplyRegexButton.Click += new System.EventHandler(this.ApplyRegexButton_Click);
            // 
            // PostRegexListBox
            // 
            this.PostRegexListBox.FormattingEnabled = true;
            this.PostRegexListBox.HorizontalScrollbar = true;
            this.PostRegexListBox.Location = new System.Drawing.Point(416, 108);
            this.PostRegexListBox.Name = "PostRegexListBox";
            this.PostRegexListBox.ScrollAlwaysVisible = true;
            this.PostRegexListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.PostRegexListBox.Size = new System.Drawing.Size(465, 498);
            this.PostRegexListBox.TabIndex = 15;
            // 
            // FilesFoundListBox
            // 
            this.FilesFoundListBox.FormattingEnabled = true;
            this.FilesFoundListBox.HorizontalScrollbar = true;
            this.FilesFoundListBox.Location = new System.Drawing.Point(9, 108);
            this.FilesFoundListBox.Name = "FilesFoundListBox";
            this.FilesFoundListBox.ScrollAlwaysVisible = true;
            this.FilesFoundListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.FilesFoundListBox.Size = new System.Drawing.Size(401, 498);
            this.FilesFoundListBox.TabIndex = 5;
            // 
            // RegExFileRenamer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 656);
            this.Controls.Add(this.FilesFoundListBox);
            this.Controls.Add(this.PostRegexListBox);
            this.Controls.Add(this.ApplyRegexButton);
            this.Controls.Add(this.FolderScannedCheckBox);
            this.Controls.Add(this.RegexTestedCheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ReplacementTextBox);
            this.Controls.Add(this.TestRegexButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RegexTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ScanDirectoryButton);
            this.Controls.Add(this.OpenExplorerButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DirectoryTextBox);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "RegExFileRenamer";
            this.Text = "RegExFileRenamer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DirectoryTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OpenExplorerButton;
        private System.Windows.Forms.Button ScanDirectoryButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox RegexTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button TestRegexButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ReplacementTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox RegexTestedCheckBox;
        private System.Windows.Forms.CheckBox FolderScannedCheckBox;
        private System.Windows.Forms.Button ApplyRegexButton;
        private System.Windows.Forms.ListBox PostRegexListBox;
        private System.Windows.Forms.ListBox FilesFoundListBox;
    }
}

