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
        TextBox MainRegexTextBox;
        TextBox MainReplacementTextBox;

        //Setup when opened from MainWindow
        public LoadRegexWindow(SavedRegexesClass LoadedRegexes,TextBox MainRegexTextbox,TextBox MainReplacementTextbox)
        {
            InitializeComponent();
            //Save info from the calling window
            LoadedSave = LoadedRegexes;
            MainRegexTextBox = MainRegexTextbox;
            MainReplacementTextBox = MainReplacementTextbox;
            //Display titles of the loaded regexes
            foreach(SavedRegex Regex in LoadedSave.SavedRegexList)
            {
                LoadedRegexesListBox.Items.Add(Regex.Title);
            }
            LoadedRegexesListBox.SelectedIndex = 0;
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
        }

        //Load selected regex to the main window
        private void LoadRegexButton_Click(object sender, RoutedEventArgs e)
        {
            MainRegexTextBox.Text = RegexTextBox.Text;
            MainReplacementTextBox.Text = ReplacementTextBox.Text;
            Close();
        }
    }
}
