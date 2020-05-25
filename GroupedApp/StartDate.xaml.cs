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

namespace GroupedApp
{
    /// <summary>
    /// Interaction logic for StartDate.xaml
    /// </summary>
    public partial class StartDate : Window
    {
        public DateTime Returndate { get; set; }
        public StartDate()
        {
            InitializeComponent();
        }

        public void Accepted(object snd, RoutedEventArgs s)
        {
            DateTime? x = StartDateEntry.SelectedDate;
            if (x.HasValue)
            {
                Returndate = (DateTime)x;
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
