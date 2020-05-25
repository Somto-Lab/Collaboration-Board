using Microsoft.Win32;
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
using System.IO;

namespace GroupedApp
{
    /// <summary>
    /// Interaction logic for PDFViewer.xaml
    /// </summary>
    public partial class PDFViewer : Window
    {
        public List<string> FilePaths { get; set; }


        public PDFViewer()
        {
            InitializeComponent();
        }

        public void LaunchFile(object snd, RoutedEventArgs s)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = false;
            openFile.Filter = "PDF Files(*.pdf)|*.pdf";
            if (openFile.ShowDialog() == true)
            {
                View.Path = openFile.FileName;
            }
        }

    }
}
