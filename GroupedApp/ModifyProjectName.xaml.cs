using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ModifyProjectName.xaml
    /// </summary>
    public partial class ModifyProjectName : Window
    {

        public bool DeleteProject { get; set; }
        public bool ConfirmProject{get; set;}
        public ModifyProjectName(string EntryText)
        {
            InitializeComponent();
            ProjectNameEntry.Text = EntryText;
            DeleteProject = false;
            ConfirmProject = false;
        }

        private void Delete(object snd, RoutedEventArgs s)
        {
            string DeleteMessage = "This will delete all entries in the specified project. Do you want to proceed?";
            MessageBoxResult result = MessageBox.Show(this, DeleteMessage, "Delete Project", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            switch (result)
            {
                case MessageBoxResult.OK:
                    DeleteProject = true;
                    DialogResult = true;
                    break;
                case MessageBoxResult.Cancel:
                    DialogResult = false;
                    break;
                default:
                    DialogResult = false;
                    break;
            }
            Close();
        }

        private void Confirm(object snd, RoutedEventArgs s)
        {
            if (ProjectNameEntry.Text != "" && ProjectNameEntry.Text.All(char.IsLetterOrDigit))
            {
                if (ProjectNameEntry.Text.Equals("admin", StringComparison.InvariantCultureIgnoreCase) ||
                    ProjectNameEntry.Text.Equals("local", StringComparison.InvariantCultureIgnoreCase) ||
                    ProjectNameEntry.Text.Equals("config", StringComparison.InvariantCultureIgnoreCase))
                {
                    MessageBox.Show("'admin', 'local', and 'config' are forbidden project names. Please enter a project name.");
                }
                else
                {
                    ConfirmProject = true;
                    DialogResult = true;
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Enter a valid project name.");
            }   
        }
    }
}
