﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace RegExFileRenamer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Open Windows Explorer UI to select a file directory
        private void OpenExplorerButton_Click(object sender, RoutedEventArgs e)
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
                    FolderScannedCheckBox.IsChecked = false;
                    FilesFoundListBox.Items.Clear();
                    //Reset whether the regex has been tested
                    RegexTestedCheckBox.IsChecked = false;
                    PostRegexListBox.Items.Clear();
                    //Scan the folder
                    ScanDirectoryButton_Click(sender, e);
                }
            }
        }

        //Scan directory for all files
        private void ScanDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //scan the folder for all file names
                string[] Files = Directory.GetFiles(DirectoryTextBox.Text);

                //add just the file names to the user UI
                FilesFoundListBox.Items.Clear();
                foreach (string file in Files)
                {
                    //add file name to the user display
                    FilesFoundListBox.Items.Add(file.Replace(@DirectoryTextBox.Text + @"\", string.Empty));
                }

                //Set whether the folder has been scanned
                FolderScannedCheckBox.IsChecked = true;
                //Reset whether the regex has been tested
                RegexTestedCheckBox.IsChecked = false;
                PostRegexListBox.Items.Clear();
            }
            catch (Exception Except)
            {
                MessageBox.Show("Failed to scan folder: " + Except.Message);
            }
        }

        //Test the regular expression and replacement
        private void TestRegexButton_Click(object sender, RoutedEventArgs e)
        {
            if (FolderScannedCheckBox.IsChecked == true)
            {
                try
                {
                    PostRegexListBox.Items.Clear();
                    //Apply regex and replacement to all found file names and display them for the user to review
                    foreach (string FileName in FilesFoundListBox.Items)
                    {
                        PostRegexListBox.Items.Add(Regex.Replace(FileName, RegexTextBox.Text, ReplacementTextBox.Text));
                    }
                    //Set whether the regex has been tested
                    RegexTestedCheckBox.IsChecked = true;
                }
                catch (Exception Except)
                {
                    PostRegexListBox.Items.Clear();
                    MessageBox.Show("Regex failed: " + Except.Message);
                }
            }
            else
            {
                MessageBox.Show("Scan a folder before testing the regular expression.");
            }
        }

        //Apply the regular expression and replacement to all found files
        private void ApplyRegexButton_Click(object sender, RoutedEventArgs e)
        {
            //check if the folder has been scanned and if the regex has been tested
            if (FolderScannedCheckBox.IsChecked == true && RegexTestedCheckBox.IsChecked == true)
            {
                //rename files one by one
                for (int i = 0; i < FilesFoundListBox.Items.Count; i++)
                {
                    try
                    {
                        string OldNameFullPath = DirectoryTextBox.Text + "\\" + FilesFoundListBox.Items[i];
                        string NewNameFullPath = DirectoryTextBox.Text + "\\" + PostRegexListBox.Items[i];
                        File.Move(OldNameFullPath, NewNameFullPath);
                    }
                    catch (Exception Except)
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

        //Reset all safety checks on directory change
        private void DirectoryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Reset whether the folder has been scanned
            FolderScannedCheckBox.IsChecked = false;
            FilesFoundListBox.Items.Clear();
            //Reset whether the regex has been tested
            RegexTestedCheckBox.IsChecked = false;
            PostRegexListBox.Items.Clear();
        }

        //Reset regex safety check on expression change
        private void RegexTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Reset whether the regex has been tested
            RegexTestedCheckBox.IsChecked = false;
            PostRegexListBox.Items.Clear();
        }

        //Reset regex safety check on replacement change
        private void ReplacementTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Reset whether the regex has been tested
            RegexTestedCheckBox.IsChecked = false;
            PostRegexListBox.Items.Clear();
        }

        //Sync the scroll bars on the ListBoxes
        private void FilesFoundScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            PostRegexScrollViewer.ScrollToVerticalOffset(FilesFoundScrollViewer.VerticalOffset);
            PostRegexScrollViewer.ScrollToHorizontalOffset(FilesFoundScrollViewer.HorizontalOffset);
        }

        //Sync the scroll bars on the ListBoxes
        private void PostRegexScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            FilesFoundScrollViewer.ScrollToVerticalOffset(PostRegexScrollViewer.VerticalOffset);
            FilesFoundScrollViewer.ScrollToHorizontalOffset(PostRegexScrollViewer.HorizontalOffset);
        }

        //Weird thing to make the scroll wheel with the FilesFoundListBox
        private void FilesFoundListBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            FilesFoundScrollViewer.ScrollToVerticalOffset(FilesFoundScrollViewer.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        //Weird thing to make the scroll wheel with the FilesFoundListBox
        private void PostRegexListBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            PostRegexScrollViewer.ScrollToVerticalOffset(PostRegexScrollViewer.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        //Sync the selections on the ListBoxes
        private void FilesFoundListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PostRegexListBox.SelectedIndex = FilesFoundListBox.SelectedIndex;
        }

        //Sync the selections on the ListBoxes
        private void PostRegexListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilesFoundListBox.SelectedIndex = PostRegexListBox.SelectedIndex;
        }
    }
}
