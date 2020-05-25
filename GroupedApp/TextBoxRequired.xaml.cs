using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    /// Interaction logic for TextBoxRequired.xaml
    /// </summary>
    public partial class TextBoxRequired : UserControl
    {
        public string _textboxentry;
        public TextBoxRequired()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public string TextBoxEntry
        {
            get { return _textboxentry; }
            set { _textboxentry = value; NotifyPropertyChanged("TextBoxEntry"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //below used to control amount of text required and display with label
        //public string TextBoxTitle { get; set; }

        //public int TextBoxMaximumLength { get; set; }

    }



    public class TextBoxValidate : ValidationRule
    {
        public string Error = "Required";

        public int TextBoxMinimumLength { get; set; } = 1;

        public string ErrorMessage { get { return Error; } set { Error = value; } }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string inputString = (value ?? string.Empty).ToString();
            if (inputString.Length < this.TextBoxMinimumLength || value == null)
            {
                return new ValidationResult(false, this.ErrorMessage);
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }

    }
}




