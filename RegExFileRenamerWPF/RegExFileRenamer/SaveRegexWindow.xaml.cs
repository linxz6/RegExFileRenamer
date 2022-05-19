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
    /// Interaction logic for SaveRegexWindow.xaml
    /// </summary>
    public partial class SaveRegexWindow : Window
    {
        SavedRegexesClass LoadedSave;

        public SaveRegexWindow(SavedRegexesClass LoadedRegexes,string NewRegex,string NewReplacement)
        {
            InitializeComponent();
            //Save info from the calling window
            LoadedSave = LoadedRegexes;
            RegexTextBox.Text = NewRegex;
            ReplacementTextBox.Text = NewReplacement;
            //Display titles of the loaded regexes
            foreach (SavedRegex Regex in LoadedSave.SavedRegexList)
            {
                LoadedRegexesListBox.Items.Add(Regex.Title);
            }
            TitleTextBox.Focus();
            TitleTextBox.SelectionStart = 0;
            TitleTextBox.SelectionLength = TitleTextBox.Text.Count();
        }

        private void SaveRegexButton_Click(object sender, RoutedEventArgs e)
        {
            //check if title exists
            if(string.IsNullOrWhiteSpace(TitleTextBox.Text) == true)
            {
                MessageBox.Show("Title can't be only whitespace");
                return;
            }

            //check if title has already been used
            foreach(SavedRegex ExistingRegex in LoadedSave.SavedRegexList)
            {
                if(ExistingRegex.Title == TitleTextBox.Text)
                {
                    MessageBox.Show("Regex with that title already exists");
                    return;
                }
            }

            try
            {
                //create new regex
                SavedRegex NewRegex = new SavedRegex();
                NewRegex.Title = TitleTextBox.Text;
                NewRegex.Regex = RegexTextBox.Text;
                NewRegex.Replacement = ReplacementTextBox.Text;
                NewRegex.Description = DescriptionTextBox.Text;
                //save the file
                LoadedSave.SavedRegexList.Add(NewRegex);
                LoadedSave.Save(MainWindow.SavedRegexesFileName);
                //close the dialog window
                Close();
            }
            catch (Exception Err)
            {
                MessageBox.Show("Failed to save: " + Err.Message);
            }
        }

        //detect if enter key was pressed
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //treat enter key as same as save button
            if(e.Key == Key.Enter)
            {
                SaveRegexButton_Click(sender, e);
            }
        }
    }
}
