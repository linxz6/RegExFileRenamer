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
            //Load regex settings from last time
            DirectoryTextBox.Text = Properties.Settings.Default.FileDirectorySetting;
            RegexTextBox.Text = Properties.Settings.Default.RegexSetting;
            ReplacementTextBox.Text = Properties.Settings.Default.ReplacementSetting;
            OptionIgnoreCaseCheckBox.IsChecked = (bool?)Properties.Settings.Default.IgnoreCase;
            OptionExplicitCaptureCheckBox.IsChecked = (bool?)Properties.Settings.Default.ExplicitCapture;
            OptionCompiledCheckBox.IsChecked = (bool?)Properties.Settings.Default.Compiled;
            OptionIgnorePatternWhitespaceCheckBox.IsChecked = (bool?)Properties.Settings.Default.IgnorePatternWhitespace;
            OptionRightToLeftCheckBox.IsChecked = (bool?)Properties.Settings.Default.RightToLeft;
            OptionCultureInvariantCheckBox.IsChecked = (bool?)Properties.Settings.Default.CultureInvariant;
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
                    //create regex object
                    Regex Renamer = new Regex(RegexTextBox.Text, ParseRegexOptions().ConvertToEnum());
                    //Apply regex and replacement to all found file names and display them for the user to review
                    for(int i = 0;i < FilesFoundListBox.Items.Count;i++)
                    { 
                        string PostRegexFileName = string.Empty;
                        bool regexMatched = false;
                        // add * char to beginning of filename if the regex finds a match
                        if(Renamer.IsMatch(FilesFoundListBox.Items[i].ToString()))
                        {
                            PostRegexFileName += "*";
                            regexMatched = true;
                        }
                        PostRegexFileName += Renamer.Replace(FilesFoundListBox.Items[i].ToString(), ReplacementTextBox.Text);

                        PostRegexListBox.Items.Add(PostRegexFileName);

                        //highlight the chosen item if it was matched
                        if(regexMatched)
                        {
                            PostRegexListBox.SelectedItems.Add(PostRegexListBox.Items[i]);
                            FilesFoundListBox.SelectedItems.Add(FilesFoundListBox.Items[i]);
                        }
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
            //only sync the FilesFound if the post regex hasn't been cleared
            if (PostRegexListBox.Items.Count > 0)
            {
                FilesFoundScrollViewer.ScrollToVerticalOffset(PostRegexScrollViewer.VerticalOffset);
                FilesFoundScrollViewer.ScrollToHorizontalOffset(PostRegexScrollViewer.HorizontalOffset);
            }
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
            var SaveRegexDialog = new SaveRegexWindow(LoadedSave, RegexTextBox.Text, ReplacementTextBox.Text,ParseRegexOptions());
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
            var LoadRegexDialog = new LoadRegexWindow(LoadedSave,this);
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

        //Save the regex settings on close
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.FileDirectorySetting = DirectoryTextBox.Text;
            Properties.Settings.Default.RegexSetting = RegexTextBox.Text;
            Properties.Settings.Default.ReplacementSetting = ReplacementTextBox.Text;
            Properties.Settings.Default.IgnoreCase = (bool)OptionIgnoreCaseCheckBox.IsChecked;
            Properties.Settings.Default.ExplicitCapture = (bool)OptionExplicitCaptureCheckBox.IsChecked;
            Properties.Settings.Default.Compiled = (bool)OptionCompiledCheckBox.IsChecked;
            Properties.Settings.Default.IgnorePatternWhitespace = (bool)OptionIgnorePatternWhitespaceCheckBox.IsChecked;
            Properties.Settings.Default.RightToLeft = (bool)OptionRightToLeftCheckBox.IsChecked;
            Properties.Settings.Default.CultureInvariant = (bool)OptionCultureInvariantCheckBox.IsChecked;
            Properties.Settings.Default.Save();
        }
       
        //parse regex options
        private RegexOptionsChoices ParseRegexOptions()
        {
            RegexOptionsChoices FoundOptions = new RegexOptionsChoices((bool)OptionIgnoreCaseCheckBox.IsChecked, (bool)OptionExplicitCaptureCheckBox.IsChecked, (bool)OptionCompiledCheckBox.IsChecked, (bool)OptionIgnorePatternWhitespaceCheckBox.IsChecked, (bool)OptionRightToLeftCheckBox.IsChecked, (bool)OptionCultureInvariantCheckBox.IsChecked);

            return FoundOptions;
        }

        //set regex options
        public bool SetRegexOptions(RegexOptionsChoices Options)
        {
            OptionIgnoreCaseCheckBox.IsChecked = Options.IgnoreCase;
            OptionExplicitCaptureCheckBox.IsChecked = Options.ExplicitCapture;
            OptionCompiledCheckBox.IsChecked = Options.Compiled;
            OptionIgnorePatternWhitespaceCheckBox.IsChecked = Options.IgnorePatternWhitespace;
            OptionRightToLeftCheckBox.IsChecked = Options.RightToLeft;
            OptionCultureInvariantCheckBox.IsChecked = Options.CultureInvariant;
            return true;
        }

        //reset the regex testing after option changes
        private void OptionIgnoreCaseCheckBox_Click(object sender, RoutedEventArgs e)
        {
            //Reset whether the regex has been tested
            RegexTestedCheckBox.IsChecked = false;
            PostRegexListBox.Items.Clear();
        }
        private void OptionExplicitCaptureCheckBox_Click(object sender, RoutedEventArgs e)
        {
            //Reset whether the regex has been tested
            RegexTestedCheckBox.IsChecked = false;
            PostRegexListBox.Items.Clear();
        }
        private void OptionCompiledCheckBox_Click(object sender, RoutedEventArgs e)
        {
            //Reset whether the regex has been tested
            RegexTestedCheckBox.IsChecked = false;
            PostRegexListBox.Items.Clear();
        }
        private void OptionIgnorePatternWhitespaceCheckBox_Click(object sender, RoutedEventArgs e)
        {
            //Reset whether the regex has been tested
            RegexTestedCheckBox.IsChecked = false;
            PostRegexListBox.Items.Clear();
        }
        private void OptionRightToLeftCheckBox_Click(object sender, RoutedEventArgs e)
        {
            //Reset whether the regex has been tested
            RegexTestedCheckBox.IsChecked = false;
            PostRegexListBox.Items.Clear();
        }
        private void OptionCultureInvariantCheckBox_Click(object sender, RoutedEventArgs e)
        {
            //Reset whether the regex has been tested
            RegexTestedCheckBox.IsChecked = false;
            PostRegexListBox.Items.Clear();
        }
    }

    public class SavedRegex
    {
        public string Title;
        public string Regex;
        public string Replacement;
        public RegexOptionsChoices Options;
        public string Description;

        public override string ToString()
        {
            return Title;
        }
    }

    public class RegexOptionsChoices
    {
        public bool IgnoreCase = false;
        public bool Multiline = false;
        public bool ExplicitCapture = false;
        public bool Compiled = false;
        public bool Singleline = false;
        public bool IgnorePatternWhitespace = false;
        public bool RightToLeft = false;
        public bool ECMAScript = false;
        public bool CultureInvariant = false;

        public RegexOptionsChoices() { }

        public RegexOptionsChoices(bool ignoreCase,bool explicitCapture,bool compiled,bool ignorePatternWhitespace,bool righttoLeft,bool cultureInvariant)
        {
            IgnoreCase = ignoreCase;
            ExplicitCapture = explicitCapture;
            Compiled = compiled;
            IgnorePatternWhitespace = ignorePatternWhitespace;
            RightToLeft = righttoLeft;
            CultureInvariant = cultureInvariant;
        }

        public RegexOptions ConvertToEnum()
        {
            RegexOptions FinishedEnum = RegexOptions.None;
            if (IgnoreCase)
            {
                FinishedEnum = FinishedEnum | RegexOptions.IgnoreCase;
            }
            if (Multiline)
            {
                FinishedEnum = FinishedEnum | RegexOptions.Multiline;
            }
            if (ExplicitCapture)
            {
                FinishedEnum = FinishedEnum | RegexOptions.ExplicitCapture;
            }
            if (Compiled)
            {
                FinishedEnum = FinishedEnum | RegexOptions.Compiled;
            }
            if (Singleline)
            {
                FinishedEnum = FinishedEnum | RegexOptions.Singleline;
            }
            if (IgnorePatternWhitespace)
            {
                FinishedEnum = FinishedEnum | RegexOptions.IgnorePatternWhitespace;
            }
            if (RightToLeft)
            {
                FinishedEnum = FinishedEnum | RegexOptions.RightToLeft;
            }
            if (ECMAScript)
            {
                FinishedEnum = FinishedEnum | RegexOptions.ECMAScript;
            }
            if (CultureInvariant)
            {
                FinishedEnum = FinishedEnum | RegexOptions.CultureInvariant;
            }
            return FinishedEnum;
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
