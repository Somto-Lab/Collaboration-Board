using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.Data.Pdf;
using Windows.Storage;
using Windows.Storage.Streams;



//Thanks to Lander Verhack, base code gotten from https://blogs.u2u.be/lander/post/2018/01/23/Creating-a-PDF-Viewer-in-WPF-using-Windows-10-APIs
    //with minor changes mine to suit need and design
    
namespace GroupedApp
{
    /// <summary>
    /// Interaction logic for PDF.xaml
    /// </summary>
    public partial class PDF : UserControl
    {
        public PDF()
        {
            InitializeComponent();
        }
        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        public static readonly DependencyProperty PathProperty = DependencyProperty.Register("Path", 
            typeof(string), typeof(PDF), new PropertyMetadata(null, propertyChangedCallback: PathChanged));

        private static void PathChanged(DependencyObject obj, DependencyPropertyChangedEventArgs eventArgs)
        {
            var Draw = (PDF)obj;

            if (!string.IsNullOrEmpty(Draw.Path))
            {
                var PDFpath = System.IO.Path.GetFullPath(Draw.Path);

                StorageFile.GetFileFromPathAsync(PDFpath).AsTask().ContinueWith(t => PdfDocument.LoadFromFileAsync(t.Result).
                AsTask()).Unwrap().ContinueWith(t2 => PdfToImages(Draw, t2.Result), TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private async static Task PdfToImages(PDF pDF, PdfDocument pdfDocument)
        {
            var items = pDF.Container.Items;
            items.Clear();

            if (pdfDocument == null) return;

            for(uint i = 0; i < pdfDocument.PageCount; i++)
            {
                using (var page = pdfDocument.GetPage(i))
                {
                    var bitmap = await PageToBitmapAsync(page);
                    var image = new Image
                    {
                        Source = bitmap,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    items.Add(image);
                }
            }
        }


        private static async Task<BitmapImage> PageToBitmapAsync(PdfPage page)
        {
            BitmapImage image = new BitmapImage();

            using (var stream = new InMemoryRandomAccessStream())
            {
                await page.RenderToStreamAsync(stream);

                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream.AsStream();
                image.EndInit();
            }

            return image;
        }
    }
}
