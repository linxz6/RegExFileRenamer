using System;
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
using System.Xml.Serialization;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace RegExFileRenamer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public string SavedRegexesFileName = "SavedRegexes.xml";
        static SavedRegexesClass LoadedSave;

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
                        string PostRegexFileName = string.Empty;
                        // add * char to beginning of filename if the regex finds a match
                        if(Regex.IsMatch(FileName, RegexTextBox.Text))
                        {
                            PostRegexFileName += "*";
                        }
                        PostRegexFileName += Regex.Replace(FileName, RegexTextBox.Text, ReplacementTextBox.Text);

                        PostRegexListBox.Items.Add(PostRegexFileName);
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
                        string NewNameFullPath = DirectoryTextBox.Text + "\\" + PostRegexListBox.Items[i].ToString().Replace("*","");
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

        //Open the saving window
        private void SaveRegexButton_Click(object sender, RoutedEventArgs e)
        {
            //Load the save file if it hasn't been already
            if (LoadedSave == null)
            {
                SavedRegexesClass.CheckIfSaveExists(SavedRegexesFileName);
                LoadedSave = SavedRegexesClass.Load(SavedRegexesFileName);
            }

            //open save window
            var SaveRegexDialog = new SaveRegexWindow(LoadedSave, RegexTextBox.Text, ReplacementTextBox.Text);
            SaveRegexDialog.Owner = this;
            SaveRegexDialog.ShowDialog();
        }

        //Open the loading window
        private void LoadRegexButton_Click(object sender, RoutedEventArgs e)
        {
            //Load the save file if it hasn't been already
            if (LoadedSave == null)
            {
                SavedRegexesClass.CheckIfSaveExists(SavedRegexesFileName);
                LoadedSave = SavedRegexesClass.Load(SavedRegexesFileName);
            }

            //Open new window to display the loaded save
            var LoadRegexDialog = new LoadRegexWindow(LoadedSave,RegexTextBox,ReplacementTextBox);
            LoadRegexDialog.Owner = this;
            LoadRegexDialog.ShowDialog();
        }

        //Open the save editing window
        private void EditSaveButton_Click(object sender, RoutedEventArgs e)
        {
            //Load the save file if it hasn't been already
            if (LoadedSave == null)
            {
                SavedRegexesClass.CheckIfSaveExists(SavedRegexesFileName);
                LoadedSave = SavedRegexesClass.Load(SavedRegexesFileName);
            }

            //Open new window to display the loaded save
            var EditSaveDialog = new EditSaveWindow(LoadedSave);
            EditSaveDialog.Owner = this;
            EditSaveDialog.ShowDialog();
        }
    }

    public class SavedRegex
    {
        public string Title;
        public string Regex;
        public string Replacement;
        public RegexOptions Options;
        public string Description;

        public override string ToString()
        {
            return Title;
        }
    }

    public class SavedRegexesClass
    {
        public List<SavedRegex> SavedRegexList;

        public void Save(string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                XmlSerializer xmls = new XmlSerializer(typeof(SavedRegexesClass));
                xmls.Serialize(sw, this);
            }
        }

        public static void CheckIfSaveExists(string filename)
        {
            //Create an empty save file if it doesn't exist
            if (File.Exists(filename) == false)
            {
                SavedRegexesClass EmptySave = new SavedRegexesClass();
                EmptySave.Save(filename);
            }
        }

        public static SavedRegexesClass Load(string filename)
        {
            using (StreamReader sw = new StreamReader(filename))
            {
                XmlSerializer xmls = new XmlSerializer(typeof(SavedRegexesClass));
                return xmls.Deserialize(sw) as SavedRegexesClass;
            }
        }
    }
}
