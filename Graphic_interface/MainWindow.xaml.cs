using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using NivelStocareDate;
using NivelWPF;
using LibrarieModele;
using System.ComponentModel;
using System.Collections.ObjectModel;
using LibrarieModele.enums;

namespace Graphic_interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IstocareDateActivities activities;
        public ObservableCollection<Activitate> UIActivities { get; set; } = new ObservableCollection<Activitate>();
        public ObservableCollection<LegendItem> LegendItems { get; set; }
        public class LegendItem
        {
            public ActivityType Type { get; set; }
            public string DisplayName { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            activities = ManagerStocare.GetAdministratorStocareActivitati();
            var initialData = activities.GetActivitiesValues();
            foreach (var act in initialData) UIActivities.Add(act);
            LegendItems = new ObservableCollection<LegendItem>
            {
                new LegendItem { Type = ActivityType.SelfImprovement, DisplayName = "Dezvoltare personală" },
                new LegendItem { Type = ActivityType.Learning, DisplayName = "Învățare" },
                new LegendItem { Type = ActivityType.Project, DisplayName = "Proiect" },
                new LegendItem { Type = ActivityType.Work, DisplayName = "Muncă" },
                new LegendItem { Type = ActivityType.Sport, DisplayName = "Sport" },
                new LegendItem { Type = ActivityType.Education, DisplayName = "Educație" },
                new LegendItem { Type = ActivityType.Resting, DisplayName = "Odihnă" },
                new LegendItem { Type = ActivityType.Entertainment, DisplayName = "Divertisment" },
                new LegendItem { Type = ActivityType.None, DisplayName = "Niciuna" },
            };
            TypesLegend.ItemsSource = LegendItems;

            ActivityListView.ItemsSource = UIActivities;
        }

        private void AddActivityWindow(object sender, RoutedEventArgs e)
        {
            AddActivityWindow dialog = new AddActivityWindow();

            if(dialog.ShowDialog() == true)
            {
                Activitate act = dialog.newAcitivity;
                activities.add_activityToList(act);
                RefreshActivities();
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
        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //performSearch();
            }
        }
    }
}