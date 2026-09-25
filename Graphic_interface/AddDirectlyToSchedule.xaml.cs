using LibrarieModele;
using LibrarieModele.enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NivelWPF
{
    /// <summary>
    /// Interaction logic for AddDirectlyToSchedule.xaml
    /// </summary>
    public partial class AddDirectlyToSchedule : Window
    {
        public Activitate SelectedActivity { get; set; }
        public Scheduled_activity Newscheduled_Activity;
        public WeekDays SelectedDay;
        private TimeOnly startTime;
        private List<Activitate> activities;
        private class DisplayDays
        {
            public WeekDays Value { get; set; }
            public string Day => Value.ToRomanianString();
        }

        public AddDirectlyToSchedule(WeekDays day, TimeOnly Starthour, List<Activitate> availableActivities)
        {
            this.startTime = Starthour;
            this.activities = availableActivities;
            this.SelectedDay = day;
            this.SelectedActivity = null;
            this.DataContext = this;
            InitializeComponent();
            PopulateTimeComboBoxes();
            SetComboBoxSources();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void AddToSched_Click(object sender, RoutedEventArgs e)
        {
            TimeOnly endTime;
            startTime = TimeOnly.Parse(StartTimeBox.Text);
            endTime = TimeOnly.Parse(EndTimeBox.Text);

            if (SelectedActivity == null)
            {
                TimeErrorLabel.Content = "Selectati o activitate.";
                return;
            }
            if (startTime > endTime)
            {
                TimeErrorLabel.Content = "Interval nevalid.";
                return;
            }
            if (DayPicker.SelectedValue == null)
            {
                TimeErrorLabel.Content = "Selectati ziua.";
                DayPicker.BorderBrush = Brushes.Red;
                return;
            }
            Newscheduled_Activity = new Scheduled_activity(SelectedActivity.ID, startTime, endTime);
            this.SelectedDay = (WeekDays)DayPicker.SelectedValue;
            this.DialogResult = true;
        }
        private void TimeBox_TextChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StartTimeBox.SelectedItem is string start &&
                    EndTimeBox.SelectedItem is string end)
            {
                startTime = TimeOnly.Parse(start);
                var endTime = TimeOnly.Parse(end);

                if (endTime > startTime)
                {
                    TimeSpan duration = endTime - startTime;
                    DurationText.Text = $"{duration.Hours}h";
                }
            }
            else
            {
                DurationText.Text = "Duratie: --";
                StartTimeBox.BorderBrush = Brushes.Red;
                EndTimeBox.BorderBrush = Brushes.Red;
            }
        }
        private void SetComboBoxSources()
        {
            List<DisplayDays> days = Enum.GetValues(typeof(WeekDays))
                .Cast<WeekDays>()
                .Select(d => new DisplayDays { Value = d })
                .ToList();
            DayPicker.ItemsSource = days;
            DayPicker.SetValue(ComboBox.SelectedValueProperty, SelectedDay);
        }
        private void PopulateTimeComboBoxes()
        {
            var times = new List<string>();
            for (int h = 8; h < 24; h++)
            {
                times.Add($"{h:D2}:00");
            }
            times.Add("00:00");

            StartTimeBox.ItemsSource = times;
            EndTimeBox.ItemsSource = times;

            StartTimeBox.Text = startTime.ToString("HH:mm");
            EndTimeBox.Text = startTime.AddHours(1).ToString("HH:mm");
        }
        private void changeActivity_Click(object sender, RoutedEventArgs e)
        {
            if (activities == null || activities.Count == 0)
            {
                MessageBox.Show("Nu sunt activitati disponibile.", "Selectare activitate", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            SelectActivityDialog dialog = new SelectActivityDialog(activities);
            if (dialog.ShowDialog() == true)
            {
                SelectedActivity = dialog.SelectedActivity;
                // Force UI update by refreshing the data context
                this.DataContext = null;
                this.DataContext = this;
            }
        }
    }
}
