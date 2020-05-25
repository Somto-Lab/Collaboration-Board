using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GroupedApp
{
    /// <summary>
    /// Interaction logic for ProjectName.xaml
    /// </summary>
    public partial class ProjectName : Window
    {
        public ProjectName()
        {
            InitializeComponent();
        }

        public void Accept(object snd, RoutedEventArgs s)
        {
            if (ProjectNameTextBox.ControlledTextBox.Text != "" && ProjectNameTextBox.ControlledTextBox.Text.All(char.IsLetterOrDigit))
            {
                if (ProjectNameTextBox.ControlledTextBox.Text.Equals("admin", StringComparison.InvariantCultureIgnoreCase)|| 
                    ProjectNameTextBox.ControlledTextBox.Text.Equals("local", StringComparison.InvariantCultureIgnoreCase) || 
                    ProjectNameTextBox.ControlledTextBox.Text.Equals("config", StringComparison.InvariantCultureIgnoreCase))
                {
                    MessageBox.Show("'admin', 'local', and 'config' are forbidden project names. Please enter a project name.");
                }
                else
                {
                    DialogResult = true;
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Please enter an appropriate project name.");
            }
        }
    }
}
