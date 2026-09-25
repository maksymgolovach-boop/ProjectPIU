using LibrarieModele;
using LibrarieModele.enums;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace NivelWPF
{
    /// <summary>
    /// Interaction logic for ModifyAct.xaml
    /// </summary>
    public class LegendItem
    {
        public ActivityType Type { get; set; }
        public string DisplayName { get; set; }
    }
    public partial class ModifyAct : Window
    {
        public Activitate ActivityToModify;
        public ObservableCollection<LegendItem> LegendItems { get; set; }
        public ModifyAct(Activitate activity)
        {
            this.ActivityToModify = activity;
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

            InitializeComponent();
            SetSources();
            populatetxtbox();
        }
        public void populatetxtbox()
        {
            ActivityName.Text = ActivityToModify.name.ToString();
            ActivityDescription.Text = ActivityToModify.description.ToString();
            cmbTipulActivitatii.SelectedValue = ActivityToModify.type;
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            if (ActivityName.Text.Length == 0)
            {
                ActivityName.BorderBrush = Brushes.Red;
                return;
            }
            if (cmbTipulActivitatii.SelectedItem == null)
            {
                cmbTipulActivitatii.BorderBrush = Brushes.Red;
                return;
            }
            ActivityType tip = (ActivityType)cmbTipulActivitatii.SelectedValue;
            ActivityToModify.name = ActivityName.Text.Trim();
            ActivityToModify.description = ActivityDescription.Text.Trim();
            ActivityToModify.type = tip;

            this.DialogResult = true;
        }
        private void SetSources()
        {
            cmbTipulActivitatii.ItemsSource = null;
            cmbTipulActivitatii.ItemsSource = LegendItems;
        }
        private void Cancel_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
