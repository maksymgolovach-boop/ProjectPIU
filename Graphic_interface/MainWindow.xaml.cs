using LibrarieModele;
using LibrarieModele.enums;
using NivelStocareDate;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Formats.Tar;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace NivelWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IstocareDateActivities activities;
        private IstocareDateOrar orar;
        private IstocareDateDeadline deadlines;
        public ObservableCollection<Activitate> UIActivities { get; set; } = new ObservableCollection<Activitate>();
        public ObservableCollection<ActivityViewItem> DisplaySchedule { get; set; } = new ObservableCollection<ActivityViewItem>();
        public ObservableCollection<Deadline> DeadlinesList { get; set; } = new ObservableCollection<Deadline>();
        public ICollectionView DeadlinesView { get; set; }
        public class LegendItem
        {
            public ActivityType Type { get; set; }
            public string DisplayName => Type.ToRomanianString();
        }

        public MainWindow()
        {
            activities = ManagerStocare.GetAdministratorStocareActivitati();
            orar = ManagerStocare.GetAdministratorStocareOrar(activities);
            deadlines = ManagerStocare.GetAdministratorStocareDeadlines();

            InitializeComponent();
            checkForExpiredDeadlines();

            var initialActivitiesData = activities.GetActivitiesValues();
            foreach (var act in initialActivitiesData) UIActivities.Add(act);

            var initialDeadlinesData = deadlines.GetDeadlinesValues();
            foreach (var act in initialDeadlinesData) DeadlinesList.Add(act);
            populate_Deadlines();
            populate_sched();
            TypesLegend.ItemsSource = Enum.GetValues(typeof(ActivityType))
                .Cast<ActivityType>()
                .Select(t => new LegendItem { Type = t });

            ActivityListView.ItemsSource = UIActivities;
            DeadlinesListView.ItemsSource = DeadlinesView;
            this.DataContext = this;
        }

        private void AddActivityWindow(object sender, RoutedEventArgs e)
        {
            AddActivityWindow dialog = new AddActivityWindow();

            if (dialog.ShowDialog() == true)
            {
                Activitate act = dialog.newAcitivity;
                activities.add_activityToList(act);
                RefreshActivities();
            }
        }
        private void checkForExpiredDeadlines()
        {
            var expiredDL = deadlines.GetExpiredDeadlines();
            if(expiredDL.Count > 0)
            {
                StringBuilder message = new StringBuilder("Urmatoarele deadline-uri au expirat:\n");
                foreach (var dl in expiredDL)
                {
                    string deleteOption = dl.DeleteAfterExpiration ? "Da" : "Nu";
                    message.AppendLine($"- {dl.Name} (Expirat pe {dl.DisplayDueDate}) O sa fie sters ? ({deleteOption})");
                }
                MessageBox.Show(message.ToString(), "Deadline-uri expirate", MessageBoxButton.OK, MessageBoxImage.Warning);
                foreach (var dl in expiredDL)
                {
                    if (dl.DeleteAfterExpiration)
                    {
                        try
                        {
                            deadlines.removeDeadline(dl);
                            DeadlinesList.Remove(dl);
                            if(activities.GetActivity(dl.ID) != null)
                            {
                                orar.RemoveAllActivities(activities.GetActivities()[dl.ID]);
                                activities.removeActivity(activities.GetActivities()[dl.ID]);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Eroare stergere", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }
        private void populate_Deadlines()
            {
            DeadlinesView = CollectionViewSource.GetDefaultView(DeadlinesList);
            DeadlinesView.SortDescriptions.Clear();
            DeadlinesView.SortDescriptions.Add(new SortDescription("DueDate", ListSortDirection.Ascending));
            DeadlinesView.Filter = item =>
            {
                var deadline = item as Deadline;
                if (deadline == null) return false;
                return true;
            };
        }
        private void SearchActivities_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchActivities.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                RefreshActivities();
                return;
            }

            var filteredActivities = activities.GetActivitiesValues()
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
            var allActivities = activities.GetActivitiesValues();

            UIActivities.Clear();
            foreach (var act in allActivities)
            {
                UIActivities.Add(act);
            }
        }

        private void MenuModify_Click(object sender, RoutedEventArgs e)
        {
            var selectedActivity = (Activitate)ActivityListView.SelectedItem;
            ModifyAct dialog = new ModifyAct(selectedActivity);

            if (dialog.ShowDialog() == true)
            {
                Activitate act = dialog.ActivityToModify;
                activities.modifyActivity(act);
                RefreshActivities();
            }
        }

        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedActivity = (Activitate)ActivityListView.SelectedItem;
            if (selectedActivity == null) return;
            MessageBoxResult result = MessageBox.Show(
            $"Sunteti siguri sa stergeti activitatea '{selectedActivity.name}'?\nToate elementele asociate cu aceasta activitate tot o sa fie sterse",
            "Confirm stergere",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    orar.RemoveAllActivities(selectedActivity);
                    if (deadlines.GetDeadline(selectedActivity.ID) != null)
                    {
                        deadlines.removeDeadline(selectedActivity.ID);
                    }
                    activities.removeActivity(selectedActivity); // sa revizionezi mai tarziu
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Eroare stergere activitate", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                RefreshActivities();
            }
        }
        private void populate_sched()
        {
            foreach (var dayEntry in orar.GetOrar())
            {
                foreach (var sched in dayEntry.Value)
                {
                    var meta = activities.GetActivities();

                    if (meta[sched.ID] != null)
                    {
                        DisplaySchedule.Add(new ActivityViewItem
                        {
                            Sched = sched,
                            Name = meta[sched.ID].name,
                            type = meta[sched.ID].type,
                            DayColumn = (int)dayEntry.Key - 1
                        });
                    }
                }
            }
        }

        private void MenuAddtoSched_Click(object sender, RoutedEventArgs e)
        {
            var selectedActivity = (Activitate)ActivityListView.SelectedItem;
            AddtoSchedule dialog = new AddtoSchedule(selectedActivity);

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var sched = dialog.Newscheduled_Activity;
                    orar.add_ScheduledActivity_toSchedule(sched, dialog.SelectedDay);
                    DisplaySchedule.Add(new ActivityViewItem
                    {
                        Sched = sched,
                        Name = activities.GetActivities()[sched.ID].name,
                        type = activities.GetActivities()[sched.ID].type,
                        DayColumn = (int)dialog.SelectedDay - 1
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Eroare adăugare în orar", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }
        private void InfoInSchedule_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var selectedItem = menuItem?.DataContext as ActivityViewItem;
            if (selectedItem == null) return;
            if (activities.GetActivity(selectedItem.Sched.ID) == null)
            {
                MessageBox.Show("Detaliile activitatii nu sunt disponibile.", "Informații indisponibile", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var activityDetails = activities.GetActivity(selectedItem.Sched.ID);
            var descriere = activityDetails.description.Length > 0 ? activityDetails.description : "Nu exista";
            MessageBoxResult result = MessageBox.Show(
                $"Activitatea '{selectedItem.Name}' \nDescrierea: {descriere}",
                "Info",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Information);
        }

        private void DeleteFromSchedule_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var selectedItem = menuItem?.DataContext as ActivityViewItem;

            if (selectedItem == null) return;

            MessageBoxResult result = MessageBox.Show(
                $"Sunteti siguri sa stergeti activitatea '{selectedItem.Name}' din orar?",
                "Confirm stergere",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    int Day = selectedItem.DayColumn + 1;
                    Enum.TryParse(Day.ToString(), out WeekDays day);
                    orar.RemoveActivityFromDay(selectedItem.Sched, day);
                    DisplaySchedule.Remove(selectedItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Eroare stergere din orar", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }
        
        private void MenutInfoDeadline_Click(object sender, RoutedEventArgs e)
        {
            var selectedDeadline = (Deadline)DeadlinesListView.SelectedItem;
            if (selectedDeadline == null) return;
            var activityName = activities.GetActivity(selectedDeadline.ID)?.name ?? "N/A";
            MessageBox.Show(
                $"Deadline: '{selectedDeadline.Name}'\nData scadenta: {selectedDeadline.DisplayDueDate}\nActivitate asociata: {activityName}",
                "Info Deadline",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void MenuDeleteDeadline_Click(object sender, RoutedEventArgs e)
        {
            var selectedDeadline = (Deadline)DeadlinesListView.SelectedItem;
            if (selectedDeadline == null) return;
            MessageBoxResult result = MessageBox.Show(
                $"Sunteti siguri sa stergeti deadline-ul '{selectedDeadline.Name}'?",
                "Confirm stergere",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    deadlines.removeDeadline(selectedDeadline);
                    DeadlinesList.Remove(selectedDeadline);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Eroare stergere deadline", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                DeadlinesView.Filter = item =>
                {
                    var deadline = item as Deadline;
                    if (deadline == null) return false;
                    return true;
                };
            }
        }
        
        private void AddDeadlineWindow(object sender, RoutedEventArgs e)
        {
            AddDeadlineWindow dialog = new AddDeadlineWindow(activities.GetActivitiesValues());
            if (dialog.ShowDialog() == true)
            {
                Deadline deadline = dialog.CurrentDeadline;
                
                deadlines.AddDeadline(deadline);
                DeadlinesList.Add(deadline);
                DeadlinesView.Filter = item =>
                {
                    var deadline = item as Deadline;
                    if (deadline == null) return false;
                    return true;
                };
            }
        }
        
    }
}