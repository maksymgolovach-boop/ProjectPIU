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

namespace Graphic_interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IstocareDateActivities activities;
        public ObservableCollection<Activitate> UIActivities { get; set; } = new ObservableCollection<Activitate>();
        public MainWindow()
        {
            InitializeComponent();
            activities = ManagerStocare.GetAdministratorStocareActivitati();
            var initialData = activities.GetActivitiesValues();
            foreach (var act in initialData) UIActivities.Add(act);

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
                performSearch();
            }
        }

        private void performSearch()
        {
            string ActivityName = SearchActivity.Text.Trim();
            if (ActivityName.Length == 0)
            {
                RefreshActivities();
                return;
            }
            var allActivities = activities.FindActivitiesByName(ActivityName);
            UIActivities.Clear();
            if (allActivities == null) return;
                foreach (var act in allActivities)
            {
                UIActivities.Add(act);
            }
        }
    }

}