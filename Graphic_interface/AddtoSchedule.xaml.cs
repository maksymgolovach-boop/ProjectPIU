using LibrarieModele;
using LibrarieModele.enums;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace NivelWPF
{
    /// <summary>
    /// Interaction logic for AddtoSchedule.xaml
    /// </summary>
    public partial class AddtoSchedule : Window
    {
        public Activitate SelectedActivity { get; set; }
        public WeekDays SelectedDay;
        public Scheduled_activity Newscheduled_Activity;
        private class DisplayDays
        {
            public WeekDays Value { get; set; }
            public string Day => Value.ToRomanianString();
        }
        public AddtoSchedule(Activitate SelectedActivity)
        {
            this.SelectedActivity = SelectedActivity;
            this.DataContext = this;
            InitializeComponent();
            PopulateTimeComboBoxes();
            SetSource();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void AddToSched_Click(object sender, RoutedEventArgs e)
        {
            TimeOnly startTime, endTime;
            startTime = TimeOnly.Parse(StartTimeBox.Text);
            endTime = TimeOnly.Parse(EndTimeBox.Text);     
            
            if (startTime > endTime)
            {
                TimeErrorLabel.Content = "Start time must be before end time.";
                return;
            }
            if(DayPicker.SelectedValue == null)
            {
                TimeErrorLabel.Content = "Please select a day.";
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
                var startTime = TimeOnly.Parse(start);
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
        private void SetSource()
        {
            List<DisplayDays> days = Enum.GetValues(typeof(WeekDays))
                .Cast<WeekDays>()
                .Select(d => new DisplayDays { Value = d })
                .ToList();
            DayPicker.ItemsSource = days;
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

            StartTimeBox.SelectedIndex = 0;
            EndTimeBox.SelectedIndex = 1;
        }
    }
}
