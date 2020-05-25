using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Interaction logic for ProjectTabs.xaml
    /// </summary>
    public partial class ProjectTabs : Window
    {

        public static MongoClient Mongo { get; set; }

        public static bool ServerConnected { get; set; }

        public static DateTime StartDateValue { get; set; }

        public static string Project { get; set; }
 
        public ProjectTabs()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OpenTab (object snd, RoutedEventArgs s)
        {
            List<string> dataBases = new List<string>();
            bool ConnectionAlive = CheckServerConnection("192.168.43.179");
            if (ConnectionAlive)
            {
                Mongo = new MongoClient("mongodb://192.168.43.179:27017");
                ServerConnected = true;
                dataBases = Mongo.ListDatabaseNames().ToList();
                if (dataBases.Contains("admin"))
                {
                    dataBases.Remove("admin");
                }
                if (dataBases.Contains("config"))
                {
                    dataBases.Remove("config");
                }
                if (dataBases.Contains("local"))
                {
                    dataBases.Remove("local");
                }
            }
            else
            {
                MessageBox.Show("Unable to load projects, check your network connection. Alternatively " +
                    "the server might be down, please try again later or create a new project", "Connecting to server",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                ServerConnected = false;
            }

            CreateProject LoadProject = new CreateProject(dataBases);
            bool? Check = LoadProject.ShowDialog();
            if ((bool)Check)
            {
                if (LoadProject.UserLoadedProject)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.LoadContentDatabase(LoadProject.SelectedDatabase);
                    ClosingTab TheTab = new ClosingTab();
                    TheTab.Title = LoadProject.SelectedDatabase;
                    TheTab.Content = mainWindow;
                    MainTab.Items.Add(TheTab);
                    TheTab.Focus();
                }
                else
                {
                    NewProject();
                }
            }
        }

        private void NewProject()
        {
            ProjectName projectName = new ProjectName();                //get project name
            bool? Result = projectName.ShowDialog();
            if (Result.HasValue)
            {
                if ((bool)Result)
                {
                    Project = projectName.ProjectNameTextBox.TextBoxEntry;

                    StartDate startDate = new StartDate();              //get project start date
                    bool? _stDate = startDate.ShowDialog();
                    if (_stDate.HasValue)
                    {
                        if ((bool)_stDate)
                        {
                            StartDateValue = startDate.Returndate;

                            MainWindow mainWindow = new MainWindow();
                            mainWindow.LoadNewProject();
                            ClosingTab TheTab = new ClosingTab();
                            TheTab.Title = Project;
                            TheTab.Content = mainWindow;
                            MainTab.Items.Add(TheTab);
                            TheTab.Focus();
                        }
                    }
                }
            }
        }

        private bool CheckServerConnection(string URL)
        {
            bool InternetAvailable = InternetConnectivity.CheckServerConnection();
            if (InternetAvailable)
            {
                try
                {
                    Ping ping = new Ping();
                    PingReply pingReply = ping.Send(URL, 1000);
                    return pingReply.Status == IPStatus.Success;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}

//tabItem.SetResourceReference(TemplateProperty, "TabDateTemplate"); used to add dynamic resource to object