using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace RegExFileRenamer
{
    public partial class RegExFileRenamer : Form
    {
        public RegExFileRenamer()
        {
            InitializeComponent();
        }

        private void OpenExplorerButton_Click(object sender, EventArgs e)
        {
            //Open file explorer for selecting the folder
            using (CommonOpenFileDialog FolderDialog = new CommonOpenFileDialog())
            {
                FolderDialog.InitialDirectory = DirectoryTextBox.Text;
                FolderDialog.AllowNonFileSystemItems = true;
                FolderDialog.Multiselect = true;               
                FolderDialog.ShowHiddenItems = true;
                FolderDialog.IsFolderPicker = true;
                FolderDialog.Title = "Select folder to scan";
                if (FolderDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    //Set the UI to the selected folder
                    DirectoryTextBox.Text = FolderDialog.FileName;
                    //Reset whether the folder has been scanned
                    FolderScannedCheckBox.Checked = false;
                    FilesFoundListBox.Items.Clear();
                    //Reset whether the regex has been tested
                    RegexTestedCheckBox.Checked = false;
                    PostRegexListBox.Items.Clear();
                    //Scan the folder
                    ScanDirectoryButton_Click(sender, e);
                }
            }
        }

        private void ScanDirectoryButton_Click(object sender, EventArgs e)
        {
            try
            {
                //scan the folder for all file names
                string[] Files = Directory.GetFiles(DirectoryTextBox.Text);
                
                //add just the file names to the user UI
                FilesFoundListBox.Items.Clear();
                foreach(string file in Files)
                {
                    //add file name to the user display
                    FilesFoundListBox.Items.Add(file.Replace(@DirectoryTextBox.Text + @"\",string.Empty));
                }

                //Set whether the folder has been scanned
                FolderScannedCheckBox.Checked = true;
                //Reset whether the regex has been tested
                RegexTestedCheckBox.Checked = false;
                PostRegexListBox.Items.Clear();
            }
            catch(Exception Except)
            {
                MessageBox.Show("Failed to scan folder: " + Except.Message);
            }
        }

        private void TestRegexButton_Click(object sender, EventArgs e)
        {
            try
            {
                PostRegexListBox.Items.Clear();
                foreach (string FileName in FilesFoundListBox.Items)
                {
                    PostRegexListBox.Items.Add(Regex.Replace(FileName, RegexTextBox.Text, ReplacementTextBox.Text));
                }
                //Set whether the regex has been tested
                RegexTestedCheckBox.Checked = true;
            }
            catch (Exception Except)
            {
                PostRegexListBox.Items.Clear();
                MessageBox.Show("Regex failed: " + Except.Message);
            }
        }

        private void ApplyRegexButton_Click(object sender, EventArgs e)
        {
            //check if the folder has been scanned and if the regex has been tested
            if(FolderScannedCheckBox.Checked == true && RegexTestedCheckBox.Checked == true)
            {
                //rename files one by one
                for(int i = 0;i < FilesFoundListBox.Items.Count;i++)
                {
                    try
                    {
                        string OldNameFullPath = DirectoryTextBox.Text + "\\" + FilesFoundListBox.Items[i];
                        string NewNameFullPath = DirectoryTextBox.Text + "\\" + PostRegexListBox.Items[i];
                        File.Move(OldNameFullPath, NewNameFullPath);
                    }
                    catch(Exception Except)
                    {
                        MessageBox.Show("Failed to rename file: " + Except.Message);
                    }
                }

                //rescan after rename
                ScanDirectoryButton_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Scan folder and test regular expression before starting renaming process.");
            }
        }

        private void DirectoryTextBox_TextChanged(object sender, EventArgs e)
        {
            //Reset whether the folder has been scanned
            FolderScannedCheckBox.Checked = false;
            FilesFoundListBox.Items.Clear();
            //Reset whether the regex has been tested
            RegexTestedCheckBox.Checked = false;
            PostRegexListBox.Items.Clear();
        }

        private void RegexTextBox_TextChanged(object sender, EventArgs e)
        {
            //Reset whether the regex has been tested
            RegexTestedCheckBox.Checked = false;
            PostRegexListBox.Items.Clear();
        }

        private void ReplacementTextBox_TextChanged(object sender, EventArgs e)
        {
            //Reset whether the regex has been tested
            RegexTestedCheckBox.Checked = false;
            PostRegexListBox.Items.Clear();
        }
    }
}
