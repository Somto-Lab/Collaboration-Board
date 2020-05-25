using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
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
    /// Interaction logic for ModifyDiscipline.xaml
    /// </summary>
    public partial class ModifyDiscipline : Window
    {
        public bool DeleteOrCancel { get; set; }
        public ModifyDiscipline(string DisciplineText, KeyValuePair<string, SolidColorBrush> AssignedColor, Dictionary<string, SolidColorBrush> ColorOfDiscipline)
        {
            InitializeComponent();
            disciplineCombo.ItemsSource = ColorOfDiscipline;
            disciplineComboLabel.Text = DisciplineText;
            disciplineCombo.SelectedIndex = disciplineCombo.Items.IndexOf(AssignedColor);
        }

        private void ChangingColor (object snd, SelectionChangedEventArgs ea)
        {
            KeyValuePair<string, SolidColorBrush> temp = (KeyValuePair<string, SolidColorBrush>)disciplineCombo.SelectedItem;
            Background = temp.Value;
            disciplineComboLabel.Background = temp.Value;
        }

        private void Confirm (object snd, RoutedEventArgs s)
        {
            DialogResult = true;
            Close();
        }

        private void Delete (object snd, RoutedEventArgs s)
        {
            string DeleteMessage = "This will delete all entries to the specified discipline. Do you want to proceed?";
            MessageBoxResult result = MessageBox.Show(this, DeleteMessage, "Delete Discipline", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            switch (result)
            {
                case MessageBoxResult.OK:
                    DeleteOrCancel = true;
                    DialogResult = false;
                    break;
                case MessageBoxResult.Cancel:
                    DeleteOrCancel = false;
                    break;
                default:
                    DeleteOrCancel = false;
                    break;
            }
            Close();
        }
    }
}
