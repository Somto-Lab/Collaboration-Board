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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GroupedApp
{
    /// <summary>
    /// Interaction logic for DisciplineColorChange.xaml
    /// </summary>
    public partial class DisciplineColorChange : UserControl
    {
        readonly Dictionary<string, SolidColorBrush> ColorOfDiscipline;
        string DisciplineText;
        KeyValuePair<string, SolidColorBrush> AssignedColor;
        public int ID { get; set; }
        public DisciplineColorChange(string DisciplineText, KeyValuePair<string, SolidColorBrush> AssignedColor, Dictionary<string, SolidColorBrush> ColorOfDiscipline)
        {
            InitializeComponent();
            this.ColorOfDiscipline = ColorOfDiscipline;
            this.DisciplineText = DisciplineText;
            disciplineComboLabel.Background = AssignedColor.Value;
            disciplineComboLabel.Text = DisciplineText;
            this.AssignedColor = AssignedColor;
        }

        public void EditDiscipline (object snd, RoutedEventArgs s)
        {
            ModifyDiscipline modifyDiscipline = new ModifyDiscipline(DisciplineText, AssignedColor, ColorOfDiscipline);
            bool? result = modifyDiscipline.ShowDialog();
            if (result.HasValue)
            {
                KeyValuePair<string, SolidColorBrush> temporary = (KeyValuePair<string, SolidColorBrush>)modifyDiscipline.disciplineCombo.SelectedItem;
                if ((bool)result)
                {
                    DisciplineChange change = new DisciplineChange
                    {
                        NewColor = temporary.Value,
                        OldDisciplineName = DisciplineText,
                        NewColorName = temporary.Key,
                        NewDiscipline = modifyDiscipline.disciplineComboLabel.Text.ToUpper(),
                        DeleteDiscipline = false,
                        Identity = ID
                    };
                    disciplineComboLabel.Background = temporary.Value;
                    AssignedColor = (KeyValuePair<string, SolidColorBrush>)modifyDiscipline.disciplineCombo.SelectedItem;
                    disciplineComboLabel.Text = modifyDiscipline.disciplineComboLabel.Text.ToUpper();
                    DisciplineText = modifyDiscipline.disciplineComboLabel.Text.ToUpper();
                    ColorIsChanging(change);
                }
                else
                {
                    if (modifyDiscipline.DeleteOrCancel)
                    {
                        DisciplineChange change = new DisciplineChange
                        {
                            NewColor = temporary.Value,
                            OldDisciplineName = DisciplineText,
                            NewColorName = temporary.Key,
                            NewDiscipline = modifyDiscipline.disciplineComboLabel.Text.ToUpper(),
                            DeleteDiscipline = true,
                            Identity = ID
                        };
                        ColorIsChanging(change);
                    }
                }
            }
        }

        protected virtual void ColorIsChanging(DisciplineChange args)
        {
            ChangingColor?.Invoke(this, args);
        }

        public event EventHandler<DisciplineChange> ChangingColor;
    }

    public class DisciplineChange : EventArgs
    {
        public string NewDiscipline { get; set; }
        public string OldDisciplineName { get; set; }
        public string NewColorName { get; set; }
        public SolidColorBrush NewColor { get; set; }
        public bool DeleteDiscipline { get; set; }
        public int Identity { get; set; }
    }
}
