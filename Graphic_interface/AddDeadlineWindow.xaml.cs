using LibrarieModele;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace NivelWPF
{
    /// <summary>
    /// Interaction logic for AddDeadlineWindow.xaml
    /// </summary>
    public partial class AddDeadlineWindow : Window
    {
        // Mock list of activities for the ComboBox
        private ObservableCollection<Activitate> _predefinedActivities = new ObservableCollection<Activitate>();
        // The current deadline object being edited
        public Deadline CurrentDeadline { get; private set; }

        public AddDeadlineWindow(List<Activitate> activities)
        {
            foreach (var activity in activities)
            {
                _predefinedActivities.Add(activity);
            }
            InitializeComponent();
            LoadData();

            // 1. Create a new deadline object
            CurrentDeadline = new Deadline();

            // 2. Set up data binding for the form fields
            // (We'll use standard explicit bindings for inputs)
            NameTextBox.SetBinding(TextBox.TextProperty, new Binding("Name") { Source = CurrentDeadline, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            DescriptionTextBox.SetBinding(TextBox.TextProperty, new Binding("Description") { Source = CurrentDeadline, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            DueDatePicker.SetBinding(DatePicker.SelectedDateProperty, new Binding("DueDate") { Source = CurrentDeadline, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            DueDatePicker.SelectedDate = DateTime.Now.AddDays(1);
        }

        private void LoadData()
        {
            ActivityComboBox.ItemsSource = _predefinedActivities;
        }

        private void ActivityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ActivityComboBox.SelectedItem is Activitate selectedActivity)
            {
                // Update the deadline object. Bindings handle updating the TextBoxes automatically.
                CurrentDeadline.Name = selectedActivity.name;
                CurrentDeadline.Description = selectedActivity.description;
                NameTextBox.Text = selectedActivity.name;
                DescriptionTextBox.Text = selectedActivity.description;
                CurrentDeadline.ID = selectedActivity.ID;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Simple validation
            if (string.IsNullOrWhiteSpace(CurrentDeadline.Name))
            {
                MessageBox.Show("Please provide at least a Deadline Name and a Due Date.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // At this point, CurrentDeadline is fully populated.
            // In a real app, you would pass this back to your main window or database.
            CurrentDeadline.DeleteAfterExpiration = DeleteAfterExpires.IsChecked ?? false;
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

