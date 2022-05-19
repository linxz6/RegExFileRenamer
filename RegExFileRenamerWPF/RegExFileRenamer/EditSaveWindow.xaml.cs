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
            LoadedRegexesListBox.Focus();
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
            //check that only one item is selected
            if(LoadedRegexesListBox.SelectedItems.Count > 1)
            {
                MessageBox.Show("Can only modify one regex at a time");
                return;
            }

            int SelectedIndex = LoadedRegexesListBox.SelectedIndex;
            //check if title exists
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text) == true)
            {
                MessageBox.Show("Title can't be only whitespace");
                return;
            }

            //check if title already in use bu another regex
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
                //if only one item is selected
                if (LoadedRegexesListBox.SelectedItems.Count == 1)
                {                   
                    //remove selected regex
                    LoadedSave.SavedRegexList.RemoveAt(SelectedIndex);
                }
                else //>1 items are selected to be deleted
                {
                    //remove all the selected items
                    foreach(string RegexTitle in LoadedRegexesListBox.SelectedItems)
                    {
                        for(int i = 0;i < LoadedSave.SavedRegexList.Count;i++)
                        {
                            if(RegexTitle == LoadedSave.SavedRegexList[i].Title)
                            {
                                LoadedSave.SavedRegexList.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }

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

        //detect if delete key was pressed in selection window
        private void LoadedRegexesListBox_KeyDown(object sender, KeyEventArgs e)
        {
            //if delete key pressed treat the same as the delete button
            if(e.Key == Key.Delete)
            {
                DeleteRegexButton_Click(sender, e);
            }
        }

        //move regex up the list
        private void MoveUpArrowButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int SelectedIndex = LoadedRegexesListBox.SelectedIndex;
                //check if the item is already at the top
                if (SelectedIndex != 0)
                {
                    SavedRegex RegexToInsert = LoadedSave.SavedRegexList[SelectedIndex];
                    LoadedSave.SavedRegexList.RemoveAt(SelectedIndex);
                    LoadedSave.SavedRegexList.Insert(SelectedIndex - 1, RegexToInsert);
                }
                else
                {
                    return;
                }

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
                LoadedRegexesListBox.SelectedIndex = SelectedIndex - 1;
                //note that change has been made
                StuffChanged = true;
            }
            catch (Exception Err)
            {
                MessageBox.Show("Error occurred when trying to move regex: " + Err.Message);
            }
        }

        //move regex down the list
        private void MoveDownArrowButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int SelectedIndex = LoadedRegexesListBox.SelectedIndex;
                //check if the item is already at the bottom
                if (SelectedIndex != LoadedSave.SavedRegexList.Count - 1)
                {
                    SavedRegex RegexToInsert = LoadedSave.SavedRegexList[SelectedIndex];                   
                    LoadedSave.SavedRegexList.Insert(SelectedIndex + 2, RegexToInsert);
                    LoadedSave.SavedRegexList.RemoveAt(SelectedIndex);
                }
                else
                {
                    return;
                }

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
                LoadedRegexesListBox.SelectedIndex = SelectedIndex + 1;
                //note that change has been made
                StuffChanged = true;
            }
            catch (Exception Err)
            {
                MessageBox.Show("Error occurred when trying to move regex: " + Err.Message);
            }
        }
    }
}
