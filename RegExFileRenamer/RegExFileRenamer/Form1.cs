using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace RegExFileRenamer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void OpenExplorerButton_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog FolderDialog = new CommonOpenFileDialog();
            FolderDialog.IsFolderPicker = true;
            if (FolderDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                DirectoryTextBox.Text = FolderDialog.FileName;
            }
        }
    }
}
