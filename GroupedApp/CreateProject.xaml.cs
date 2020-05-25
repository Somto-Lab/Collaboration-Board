using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for CreateProject.xaml
    /// </summary>
    public partial class CreateProject : Window
    {
        public string SelectedDatabase { get; set; }
        public bool UserLoadedProject { get; set; }
        public CreateProject(List<string> Database)
        {
            InitializeComponent();
            ProjectList.ItemsSource = Database;
        }

        private void New(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            UserLoadedProject = false;
            Close();
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            if (ProjectList.SelectedItem != null)
            {
                SelectedDatabase = ProjectList.SelectedItem.ToString();
                DialogResult = true;
                UserLoadedProject = true;
                Close();
            }
        }
    }
}
