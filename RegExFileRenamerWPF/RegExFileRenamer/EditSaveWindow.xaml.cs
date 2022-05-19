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
    /// Interaction logic for EditSaveWindow.xaml
    /// </summary>
    public partial class EditSaveWindow : Window
    {
        SavedRegexesClass LoadedSave;
        bool StuffChanged = false;

        //Setup when opened from MainWindow
        public EditSaveWindow(SavedRegexesClass LoadedRegexes)
        {
            InitializeComponent();
            //Save info from the calling window
            LoadedSave = LoadedRegexes;
            //Display titles of the loaded regexes
            foreach (SavedRegex Regex in LoadedSave.SavedRegexList)
            {
                LoadedRegexesListBox.Items.Add(Regex.Title);
            }
            LoadedRegexesListBox.SelectedIndex = 0;
        }

        //Update display when user selection changes
        private void LoadedRegexesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Deal with clearing the listbox
            if (LoadedRegexesListBox.Items.Count == 0)
            {
                return;
            }
            //update user displays
            TitleTextBox.Text = LoadedSave.SavedRegexList[LoadedRegexesListBox.SelectedIndex].Title;
            RegexTextBox.Text = LoadedSave.SavedRegexList[LoadedRegexesListBox.SelectedIndex].Regex;
            ReplacementTextBox.Text = LoadedSave.SavedRegexList[LoadedRegexesListBox.SelectedIndex].Replacement;
            DescriptionTextBox.Text = LoadedSave.SavedRegexList[LoadedRegexesListBox.SelectedIndex].Description;
        }

        //Save the users edits
        private void SaveEditsButton_Click(object sender, RoutedEventArgs e)
        {
            int SelectedIndex = LoadedRegexesListBox.SelectedIndex;
            //check if title exists
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text) == true)
            {
                MessageBox.Show("Title can't be only whitespace");
                return;
            }

            //check if title has already been used
            for(int i = 0;i < LoadedSave.SavedRegexList.Count;i++)
            {
                if (LoadedSave.SavedRegexList[i].Title == TitleTextBox.Text && i != SelectedIndex)
                {
                    MessageBox.Show("Regex with that title already exists");
                    return;
                }
            }

            try
            {
                //change regex values
                LoadedSave.SavedRegexList[SelectedIndex].Title = TitleTextBox.Text;
                LoadedSave.SavedRegexList[SelectedIndex].Regex = RegexTextBox.Text;
                LoadedSave.SavedRegexList[SelectedIndex].Replacement = ReplacementTextBox.Text;
                LoadedSave.SavedRegexList[SelectedIndex].Description = DescriptionTextBox.Text;
                //refresh the display
                LoadedRegexesListBox.Items.Clear();
                foreach (SavedRegex Regex in LoadedSave.SavedRegexList)
                {
                    LoadedRegexesListBox.Items.Add(Regex.Title);
                }
                //put selection back
                LoadedRegexesListBox.SelectedIndex = SelectedIndex;
                //note that change has been made
                StuffChanged = true;
            }
            catch (Exception Err)
            {
                MessageBox.Show("Failed to save: " + Err.Message);
            }
        }

        //Delete the selected regex
        private void DeleteRegexButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int SelectedIndex = LoadedRegexesListBox.SelectedIndex;
                //remove selected regex
                LoadedSave.SavedRegexList.RemoveAt(SelectedIndex);
                //refresh user display
                LoadedRegexesListBox.Items.Clear();
                TitleTextBox.Text = string.Empty;
                RegexTextBox.Text = string.Empty;
                ReplacementTextBox.Text = string.Empty;
                DescriptionTextBox.Text = string.Empty;
                foreach (SavedRegex Regex in LoadedSave.SavedRegexList)
                {
                    LoadedRegexesListBox.Items.Add(Regex.Title);
                }
                //put selection back
                LoadedRegexesListBox.SelectedIndex = SelectedIndex;
                //note that change has been made
                StuffChanged = true;
            }
            catch (Exception Err)
            {
                MessageBox.Show("Failed to delete: " + Err.Message);
            }
        }

        //save the file if saved regexes were changed
        private void Window_Closed(object sender, EventArgs e)
        {
            if (StuffChanged == true)
            {
                LoadedSave.Save(MainWindow.SavedRegexesFileName);
            }
        }
    }
}
