using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for Content.xaml
    /// </summary>
    public partial class UserContent : Window
    {
        public string ReturnDiscipline { get; set; }
        public string ReturnContent { get; set; }
        public DateTime? ReturnStartDate { get; set; }
        public DateTime? ReturnEndDate { get; set; }
        public KeyValuePair<string, SolidColorBrush> ReturnDiscColor { get; set; }
        public bool MileCheck { get; set; }
        public bool _ReturnspanExist { get; set; }

        Dictionary<string, string> DisciplineColorsContent = new Dictionary<string, string>();      //disctionary pairing discipline with color name
        Dictionary<string, SolidColorBrush> DisciplineColors = new Dictionary<string, SolidColorBrush>();   //dictionary pairing color name with color

        bool _edit = false; //_discbool = false;


        public UserContent()
        {
            InitializeComponent();
        }

        public UserContent(DateTime _startdatepicker, Dictionary<string, string> disciplines, Dictionary<string, SolidColorBrush> _discCol)
        {
            InitializeComponent();

            StartDateCntntBox.DisplayDateStart = _startdatepicker;
            EndDateCntntBox.DisplayDateStart = _startdatepicker;

            discBox.ItemsSource = disciplines.Keys;
            DisciplineColorsContent = disciplines;
            DisciplineColors = _discCol;
            discColor.ItemsSource = _discCol;

            if (MileCheck == true)
                MilestoneCheckbox.IsChecked = true;
            _edit = false;
        }

        public UserContent(DateTime _startdatepicker, Collab IncomingContent, Dictionary<string, string> disciplines, Dictionary<string, SolidColorBrush> _discCol)
        {
            InitializeComponent();

            StartDateCntntBox.DisplayDateStart = _startdatepicker;
            EndDateCntntBox.DisplayDateStart = _startdatepicker;
            StartDateCntntBox.SelectedDate = IncomingContent.StartDate;
            EndDateCntntBox.SelectedDate = IncomingContent.EndDate;

            discBox.ItemsSource = disciplines.Keys;
            DisciplineColorsContent = disciplines;
            DisciplineColors = _discCol;
            discColor.ItemsSource = _discCol;

            discBox.SelectedIndex = discBox.Items.IndexOf(IncomingContent.Discipline);
            discColor.SelectedIndex = discColor.Items.IndexOf(IncomingContent.DisciplineColor);
            ContentBoxEntry.TextBoxEntry = IncomingContent.Content;
            
            discColor.IsEnabled = false;
            _edit = true;
        }

        public void ConfirmPopup(object snd, RoutedEventArgs s)
        {
            //check if days is more than 1
            if (!(StartDateCntntBox.SelectedDate == null || EndDateCntntBox.SelectedDate == null || StartDateCntntBox.SelectedDate == EndDateCntntBox.SelectedDate))
            {
                _ReturnspanExist = true;
            }

            //checks comboboxes and content has been entered before closing window
            if (!(discBox.Text == "" || ContentBoxEntry.ControlledTextBox.Text == "" || discColor.SelectedItem == null || StartDateCntntBox.SelectedDate == null || EndDateCntntBox.SelectedDate == null))
            {
                ReturnDiscipline = discBox.Text;
                ReturnContent = ContentBoxEntry.ControlledTextBox.Text;
                ReturnStartDate = StartDateCntntBox.SelectedDate;
                ReturnEndDate = EndDateCntntBox.SelectedDate;
                ReturnDiscColor = (KeyValuePair<string, SolidColorBrush>) discColor.SelectedItem;
                this.DialogResult = true;
                this.Close();
            }

            else if (_edit)
            {
                ReturnDiscipline = discBox.Text;
                ReturnContent = ContentBoxEntry.ControlledTextBox.Text;
                ReturnStartDate = StartDateCntntBox.SelectedDate;
                ReturnEndDate = EndDateCntntBox.SelectedDate;
                this.DialogResult = true;
                this.Close();
            }
        }

        public void ClosePopup(object snd, RoutedEventArgs s)           //potentially redundant,use of X window button
        {
            //ContentBoxEntry.ControlledTextBox.Clear(); StartDateCntntBox.SelectedDate = null; EndDateCntntBox.SelectedDate = null;
            this.DialogResult = false;
            this.Close();
        }

        public void DiscChange(object snd, SelectionChangedEventArgs s)
        {
            if (discBox.SelectedItem != null && DisciplineColorsContent.ContainsKey(discBox.SelectedItem.ToString()))
            {
                string value = DisciplineColorsContent[discBox.SelectedItem.ToString()];
                KeyValuePair<string, SolidColorBrush> DisciColor = new KeyValuePair<string, SolidColorBrush>(value, DisciplineColors[value]);
                discColor.SelectedIndex = discColor.Items.IndexOf(DisciColor);
                discColor.IsEnabled = false; //_discbool = true;
            }
            else
            {
                discColor.IsEnabled = true; discColor.SelectedItem = null;
            }
        }

        public void DateChange(object snd, SelectionChangedEventArgs s)
        {
            StartDateRectangle.Stroke = Brushes.Transparent;
            if (StartDateCntntBox.SelectedDate == null)
                StartDateRectangle.Stroke = Brushes.DarkRed;
            EndDateCntntBox.SelectedDate = null;
            DateTime temp = (DateTime)StartDateCntntBox.SelectedDate;
            EndDateCntntBox.DisplayDateStart = temp;
            EndDateCntntBox.SelectedDate = temp; 
        }

        public void DiscColorChanged(object snd, SelectionChangedEventArgs s)
        {
            discColorRectangle.Stroke = Brushes.Transparent;

            if (discColor.SelectedItem == null)
            {
                discColorRectangle.Stroke = Brushes.DarkRed;
            }
        }

        public void MilestoneCheck(object snd, RoutedEventArgs s)
        {
            if (MilestoneCheckbox.IsChecked == true)
            {
                MileCheck = true;
                EndDateCntntBox.IsEnabled = false;
                if (StartDateCntntBox.SelectedDate != null)
                    EndDateCntntBox.SelectedDate = (DateTime)StartDateCntntBox.SelectedDate;
            }
            else
            {
                MileCheck = false;
                EndDateCntntBox.IsEnabled = true;
            }
        }

        private void DiscBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            discBoxRectangle.Stroke = Brushes.Transparent;
            if (discBox.Text == "")
            {
                discBoxRectangle.Stroke = Brushes.DarkRed;
            }
        }
    }
}
