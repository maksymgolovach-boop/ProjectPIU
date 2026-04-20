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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NivelWPF
{
    /// <summary>
    /// Interaction logic for AddActivityWindow.xaml
    /// </summary>
    public partial class AddActivityWindow : Window
    {
        public Activitate newAcitivity {  get; set; }

        public AddActivityWindow()
        {
            InitializeComponent();
        }

        private void Add_click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            if(ActivityName.Text.Length == 0)
            {
                BrushIfEmpty(ActivityName);
                i++;
            }
            if (ActivityDescription.Text.Length == 0)
            {
                BrushIfEmpty(ActivityDescription);
                i++;
            }
            if (ActivityType.Text.Length == 0)
            {
                BrushIfEmpty(ActivityType);
                i++;
            }
            if( i!=0)
            {
                return;
            }

            newAcitivity = new Activitate
            {
                name = ActivityName.Text,
                description = ActivityDescription.Text,
                type = ActivityType.Text,
            };
            this.DialogResult = true;
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
