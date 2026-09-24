using LibrarieModele;
using System;
using System.Collections.Generic;
using System.Windows;

namespace NivelWPF
{
    /// <summary>
    /// Interaction logic for SelectActivityDialog.xaml
    /// </summary>
    public partial class SelectActivityDialog : Window
    {
        public Activitate SelectedActivity { get; set; }

        public SelectActivityDialog(List<Activitate> activities)
        {
            InitializeComponent();

            if (activities == null || activities.Count == 0)
            {
                ActivitiesListBox.Visibility = Visibility.Collapsed;
                EmptyMessage.Visibility = Visibility.Visible;
            }
            else
            {
                ActivitiesListBox.ItemsSource = activities;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
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
