using LibrarieModele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using LibrarieModele.enums;
using System.Collections.ObjectModel;

namespace NivelWPF
{
    /// <summary>
    /// Interaction logic for AddActivityWindow.xaml
    /// </summary>
    public partial class AddActivityWindow : Window
    {
        public Activitate newAcitivity {  get; set; }
        public ObservableCollection<LegendItem> LegendItems { get; set; }
        public class LegendItem
        {
            public ActivityType Type { get; set; }
            public string DisplayName { get; set; }
        }

        public AddActivityWindow()
        {

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
        }

        private void Add_click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            if(ActivityName.Text.Length == 0)
            {
                BrushIfEmpty(ActivityName);
                i++;
            }
            if( i!=0 )
            {
                MessageBox.Show("Introduceti numele activitatii!");
                return;
            }

            ActivityType tip = (ActivityType)cmbTipulActivitatii.SelectedValue;

            newAcitivity = new Activitate
            {
                name = ActivityName.Text,
                description = ActivityDescription.Text ?? ("--//--"),
                type = tip
            };
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

        private void BrushIfEmpty(System.Windows.Controls.TextBox lbl)
        {
            lbl.BorderBrush = Brushes.Red;
        }
    }
}
