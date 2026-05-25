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
        
        public class ActivityTypeViewModel
        {
            public ActivityType Value { get; set; }
            public string DisplayName => Value.ToRomanianString();
        }

        public AddActivityWindow()
        {
            
            InitializeComponent();
            SetSources();
        }

        private void Add_click(object sender, RoutedEventArgs e)
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

            newAcitivity = new Activitate
            {
                name = ActivityName.Text.Trim(),
                description = ActivityDescription.Text.Trim() ?? ("--//--"),
                type = (cmbTipulActivitatii.SelectedValue as ActivityType?) ?? ActivityType.None
            };
            this.DialogResult = true;
        }

        private void SetSources()
        {
            cmbTipulActivitatii.ItemsSource = null;
            cmbTipulActivitatii.ItemsSource = Enum.GetValues(typeof(ActivityType))
                            .Cast<ActivityType>()
                            .Select(t => new ActivityTypeViewModel { Value = t });
        }

        private void Cancel_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
