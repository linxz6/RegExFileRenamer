using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RegExFileRenamer
{
    /// <summary>
    /// Interaction logic for LoadRegexWindow.xaml
    /// </summary>
    public partial class LoadRegexWindow : Window
    {
        SavedRegexesClass LoadedSave;
        MainWindow Main;

        //Setup when opened from MainWindow
        public LoadRegexWindow(SavedRegexesClass LoadedRegexes, MainWindow main)
        {
            InitializeComponent();
            //Save info from the calling window
            LoadedSave = LoadedRegexes;
            Main = main;
            //Display titles of the loaded regexes
            foreach (SavedRegex Regex in LoadedSave.SavedRegexList)
            {
                LoadedRegexesListBox.Items.Add(Regex.Title);
            }
            LoadedRegexesListBox.SelectedIndex = 0;
            LoadedRegexesListBox.Focus();
        }

        //Update display when user selection changes
        private void LoadedRegexesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Deal with clearing the listbox
            if(LoadedRegexesListBox.Items.Count == 0)
            {
                return;
            }
            //update user displays
            TitleTextBox.Text = LoadedSave.SavedRegexList[LoadedRegexesListBox.SelectedIndex].Title;
            RegexTextBox.Text = LoadedSave.SavedRegexList[LoadedRegexesListBox.SelectedIndex].Regex;
            ReplacementTextBox.Text = LoadedSave.SavedRegexList[LoadedRegexesListBox.SelectedIndex].Replacement;
            DescriptionTextBox.Text = LoadedSave.SavedRegexList[LoadedRegexesListBox.SelectedIndex].Description;
            SetRegexOptions(LoadedSave.SavedRegexList[LoadedRegexesListBox.SelectedIndex].Options);
        }

        //Load selected regex to the main window
        private void LoadRegexButton_Click(object sender, RoutedEventArgs e)
        {
            Main.RegexTextBox.Text = RegexTextBox.Text;
            Main.ReplacementTextBox.Text = ReplacementTextBox.Text;
            Main.SetRegexOptions(ParseRegexOptions());
            Close();
        }

        //detect if enter key was pressed
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //treat enter key as same as load button
            if (e.Key == Key.Enter)
            {
                LoadRegexButton_Click(sender, e);
            }
        }

        //parse regex options
        private RegexOptionsChoices ParseRegexOptions()
        {
            RegexOptionsChoices FoundOptions = new RegexOptionsChoices((bool)OptionIgnoreCaseCheckBox.IsChecked, (bool)OptionExplicitCaptureCheckBox.IsChecked, (bool)OptionCompiledCheckBox.IsChecked, (bool)OptionIgnorePatternWhitespaceCheckBox.IsChecked, (bool)OptionRightToLeftCheckBox.IsChecked, (bool)OptionCultureInvariantCheckBox.IsChecked);

            return FoundOptions;
        }

        //set regex options
        public void SetRegexOptions(RegexOptionsChoices Options)
        {
            OptionIgnoreCaseCheckBox.IsChecked = Options.IgnoreCase;
            OptionExplicitCaptureCheckBox.IsChecked = Options.ExplicitCapture;
            OptionCompiledCheckBox.IsChecked = Options.Compiled;
            OptionIgnorePatternWhitespaceCheckBox.IsChecked = Options.IgnorePatternWhitespace;
            OptionRightToLeftCheckBox.IsChecked = Options.RightToLeft;
            OptionCultureInvariantCheckBox.IsChecked = Options.CultureInvariant;
        }
    }
}
