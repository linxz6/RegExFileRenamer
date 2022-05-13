namespace RegExFileRenamer
{
    partial class Form1
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
            this.SuspendLayout();
            // 
            // DirectoryTextBox
            // 
            this.DirectoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DirectoryTextBox.Location = new System.Drawing.Point(9, 26);
            this.DirectoryTextBox.Name = "DirectoryTextBox";
            this.DirectoryTextBox.Size = new System.Drawing.Size(476, 20);
            this.DirectoryTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "File Directory";
            // 
            // OpenExplorerButton
            // 
            this.OpenExplorerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenExplorerButton.Location = new System.Drawing.Point(491, 24);
            this.OpenExplorerButton.Name = "OpenExplorerButton";
            this.OpenExplorerButton.Size = new System.Drawing.Size(81, 23);
            this.OpenExplorerButton.TabIndex = 2;
            this.OpenExplorerButton.Text = "OpenExplorer";
            this.OpenExplorerButton.UseVisualStyleBackColor = true;
            this.OpenExplorerButton.Click += new System.EventHandler(this.OpenExplorerButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 471);
            this.Controls.Add(this.OpenExplorerButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DirectoryTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DirectoryTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OpenExplorerButton;
    }
}

