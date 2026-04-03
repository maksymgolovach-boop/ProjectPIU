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
using NivelStocareDate;
using LibrarieModele;
using Program;
using System.Windows.Media.Media3D;

namespace Graphic_interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IstocareDateActivities activities;
        public MainWindow()
        {
            InitializeComponent();
            activities = ManagerStocare.GetAdministratorStocareActivitati();
            
        }
        
        private void RefreshActivitiesButton(object sender, RoutedEventArgs e)
        {
            afiseazaActivitati();
            MessageBox.Show("Lista a fost actualizata!");
        }
        public void afiseazaActivitati()
        {
            var displayList = new List<string>();
            var _activities = activities.GetActivitiesValues();
            foreach (var activity in _activities)
            {
                displayList.Add(activity.INFO());
            }
            lstActivitati.ItemsSource = displayList;
        }
    }

}