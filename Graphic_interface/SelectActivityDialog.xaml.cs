using LibrarieModele;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace NivelWPF
{
    /// <summary>
    /// Interaction logic for SelectActivityDialog.xaml
    /// </summary>
    public partial class SelectActivityDialog : Window
    {
        public Activitate SelectedActivity { get; set; }
        private List<Activitate> activities;
        private ObservableCollection<Activitate> UIActivities = new ObservableCollection<Activitate>();

        public SelectActivityDialog(List<Activitate> activities)
        {
            InitializeComponent();
            this.activities = activities;

            if (this.activities == null || this.activities.Count == 0)
            {
                ActivitiesListBox.Visibility = Visibility.Collapsed;
                EmptyMessage.Visibility = Visibility.Visible;
            }
            else
            {
                ActivitiesListBox.ItemsSource = UIActivities;
            }
            RefreshActivities();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void SearchActivities_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchActivities.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                RefreshActivities();
                return;
            }

            var filteredActivities = activities
                .Where(act => act.name.ToLower().Contains(searchText))
                .ToList();

            UIActivities.Clear();
            foreach (var act in filteredActivities)
            {
                UIActivities.Add(act);
            }
        }
        private void RefreshActivities()
        {
            UIActivities.Clear();
            foreach (var act in activities)
            {
                UIActivities.Add(act);
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (ActivitiesListBox.SelectedItem is Activitate selected)
            {
                SelectedActivity = selected;
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Selectati o activitate.", "Validare", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
