using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Schema;

namespace GroupedApp
{
    public partial class MainWindow
    {

        public void LoadContentDatabase(string DatabaseName)
        {
            _projectName = DatabaseName;
            var ProjectDatabase = ProjectTabs.Mongo.GetDatabase(DatabaseName);
            var GetMisc = ProjectDatabase.GetCollection<DatabaseMisc>("DataBaseMisc");
            var filter = Builders<DatabaseMisc>.Filter.Eq("ID", 0);
            var TempList = GetMisc.Find(filter).FirstOrDefault(); 
            DatabaseMisc StartupFile = TempList as DatabaseMisc;
            
            _startdatevalueMW = DateTime.Parse(StartupFile.ProjectStartDate);
            identity = StartupFile.LastAllocatedID;
            DeletedID = StartupFile.LastDeletedList;

            DatePopulate();
            AddColorsToProject();

            var collabentry = ProjectDatabase.GetCollection<BsonDocument>("UserEntry");
            var filter2 = Builders<BsonDocument>.Filter.Empty;
            var TempEntry = collabentry.Find(filter2).ToList();

            UserContent DatabaseInput = new UserContent();

            foreach (var item in TempEntry)
            {
                DatabaseInput.ReturnDiscipline = item["ContentDiscipline"].AsString;
                DatabaseInput.ReturnContent = item["Content"].AsString;
                DatabaseInput.ReturnStartDate = DateTime.Parse(item["ContentStartDate"].AsString);
                DatabaseInput.ReturnEndDate = DateTime.Parse(item["ContentEndDate"].AsString);
                DatabaseInput.ReturnDiscColor = new KeyValuePair<string, System.Windows.Media.SolidColorBrush>(item["ContentColor"].AsString,
                    ProjectColorSelection[item["ContentColor"].AsString]);
                DatabaseInput.MileCheck = item["ContentMilestone"].ToBoolean();

                AddUserContentFromServer(DatabaseInput, item["ContentID"].ToInt32(), item["ContentCompletedState"].ToInt32());
            }

        }

        public void AddUserContentFromServer(UserContent FromServerInput, int FromServerID, int FromServerCompletedState)
        {
            bool _spanExist = FromServerInput.ReturnStartDate != FromServerInput.ReturnEndDate;

            CheckDisciplineExist(FromServerInput);

            if (FromServerInput.ReturnStartDate.HasValue)
            {
                int[] index = CheckDatesExist(FromServerInput);

                if (_spanExist)
                {
                    List<Grid> grids = GridsAddedToProject.Values.ToList();
                    foreach (Grid grid in grids)
                    {
                        RectangleAdd(grid);
                    }
                    TimeSpan temp = (TimeSpan)(FromServerInput.ReturnEndDate - FromServerInput.ReturnStartDate); int temp2 = temp.Days + 1;

                    UserAddedContents[FromServerInput.ReturnDiscipline.ToUpper()].Add(new Collab()
                    {
                        Id = FromServerID,
                        Discipline = FromServerInput.ReturnDiscipline.ToUpper(),
                        Content = FromServerInput.ReturnContent,
                        StartDate = ((DateTime)FromServerInput.ReturnStartDate).Date,
                        DisciplineColor = FromServerInput.ReturnDiscColor,
                        Row = index[0],
                        EndDate = ((DateTime)FromServerInput.ReturnEndDate).Date,
                        MainContent = true,
                        CompletedState = FromServerCompletedState,
                        MileStoneEntry = FromServerInput.MileCheck
                    });

                    AddArrowsExtension(FromServerID, FromServerInput, index);

                    TextBlock _ContentText = new TextBlock()
                    {
                        Tag = FromServerID,
                        Text = FromServerInput.ReturnContent,
                        FontSize = 11,
                        Background = FromServerInput.ReturnDiscColor.Value
                    }; TextBlockStyle(_ContentText);
                    Grid.SetRow(_ContentText, index[0]); Grid.SetColumn(_ContentText, index[1]); Grid.SetColumnSpan(_ContentText, temp2);

                    Grid ContentGrid = GridsAddedToProject[FromServerInput.ReturnDiscipline.ToUpper()];
                    ContentGrid.Children.Add(_ContentText); 
                }
                else
                {
                    List<Grid> grids = GridsAddedToProject.Values.ToList();
                    foreach (Grid grid in grids)
                    {
                        RectangleAdd(grid);
                    }
                    UserAddedContents[FromServerInput.ReturnDiscipline.ToUpper()].Add(new Collab()
                    {
                        Id = FromServerID,
                        Discipline = FromServerInput.ReturnDiscipline.ToUpper(),
                        Content = FromServerInput.ReturnContent,
                        StartDate = ((DateTime)FromServerInput.ReturnStartDate).Date,
                        DisciplineColor = FromServerInput.ReturnDiscColor,
                        Row = index[0],
                        EndDate = ((DateTime)FromServerInput.ReturnEndDate).Date,
                        MainContent = true,
                        CompletedState = FromServerCompletedState,
                        MileStoneEntry = FromServerInput.MileCheck
                    });

                    TextBlock _ContentText = new TextBlock()
                    {
                        Tag = FromServerID,
                        Text = FromServerInput.ReturnContent,
                        Background = FromServerInput.ReturnDiscColor.Value
                    }; TextBlockStyle(_ContentText);
                    Grid.SetRow(_ContentText, index[0]); Grid.SetColumn(_ContentText, index[1]);

                    Grid ContentGrid = GridsAddedToProject[FromServerInput.ReturnDiscipline.ToUpper()];
                    ContentGrid.Children.Add(_ContentText); 
                }

                switch (FromServerCompletedState)
                {
                    case 1:
                        TickFromServer(index, FromServerID);
                        break;
                    case 2:
                        CrossFromServer(index, FromServerID);
                        break;
                    case 3:
                        AddOverlapGrid(index, FromServerInput, FromServerID); StateUpdate(4, 0);
                        break;
                    default:
                        AddOverlapGrid(index, FromServerInput, FromServerID); StateUpdate(4, 0);
                        break;
                }

                if (FromServerInput.MileCheck)
                    MilestoneFunction(FromServerInput.ReturnDiscColor, FromServerInput.ReturnContent, FromServerID, ((DateTime)FromServerInput.ReturnStartDate).ToLongDateString());
            }
        }

        public void TickFromServer(int[] ServerIndex, int ServerID)
        {
            StateUpdate(1, ServerID);

            List<Collab> total = new List<Collab>();
            do
            {
                List<string> temp = UserAddedContents.Keys.ToList();
                foreach (string key in temp)
                {
                    total.AddRange(UserAddedContents[key]);
                }
            } while (false);

            int _listFirstIndex = total.FindIndex(x => x.Id == ServerID);
            Collab _editItem = total[_listFirstIndex];

            TimeSpan _span = (_editItem.EndDate - _editItem.StartDate); int _spandays = _span.Days + 1;

            Grid _overlapGrid = new Grid();

            RowDefinition row = new RowDefinition(); _overlapGrid.RowDefinitions.Add(row);
            ColumnDefinition column = new ColumnDefinition(); _overlapGrid.ColumnDefinitions.Add(column);
            _overlapGrid.Tag = ServerID;

            Button undo = new Button
            {
                Tag = ServerID,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                ToolTip = "Undo Completion",
                Content = new Image
                {
                    Width = 35,
                    Height = 35,
                    VerticalAlignment = VerticalAlignment.Center,
                    Source = new BitmapImage(new Uri("/Resources/undo.png", UriKind.Relative))
                }
            }; Grid.SetColumn(undo, 0); Grid.SetRow(undo, 0); _overlapGrid.Children.Add(undo); undo.Click += UndoClick;
            _overlapGrid.Background = Brushes.Transparent;
            Grid.SetRow(_overlapGrid, ServerIndex[0]); Grid.SetColumn(_overlapGrid, ServerIndex[1]); Grid.SetColumnSpan(_overlapGrid, _spandays);
            _overlapGrid.Style = (Style)FindResource("UndoStyle");
            GridsAddedToProject[_editItem.Discipline.ToUpper()].Children.Add(_overlapGrid);
        }

        public void CrossFromServer(int[] ServerIndex, int ServerID)
        {
            StateUpdate(2, ServerID);

            List<Collab> total = new List<Collab>();
            do
            {
                List<string> temp = UserAddedContents.Keys.ToList();
                foreach (string key in temp)
                {
                    total.AddRange(UserAddedContents[key]);
                }
            } while (false);

            int _listFirstIndex = total.FindIndex(x => x.Id == ServerID);
            Collab _editItem = total[_listFirstIndex];

            TimeSpan _span = (_editItem.EndDate - _editItem.StartDate); int _spandays = _span.Days + 1;

            Grid _overlapGrid = new Grid();

            RowDefinition row = new RowDefinition(); _overlapGrid.RowDefinitions.Add(row);
            ColumnDefinition column = new ColumnDefinition(); _overlapGrid.ColumnDefinitions.Add(column);
            _overlapGrid.Tag = ServerID;

            Button undo = new Button
            {
                Tag = ServerID,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                ToolTip = "Undo Missed",
                Content = new Image
                {
                    Width = 35,
                    Height = 35,
                    VerticalAlignment = VerticalAlignment.Center,
                    Source = new BitmapImage(new Uri("/Resources/undoCross.png", UriKind.Relative))
                }
            }; Grid.SetColumn(undo, 0); Grid.SetRow(undo, 0); _overlapGrid.Children.Add(undo); undo.Click += UndoClick;
            _overlapGrid.Background = Brushes.Transparent;
            Grid.SetRow(_overlapGrid, ServerIndex[0]); Grid.SetColumn(_overlapGrid, ServerIndex[1]); Grid.SetColumnSpan(_overlapGrid, _spandays);
            _overlapGrid.Style = (Style)FindResource("UndoStyle");
            GridsAddedToProject[_editItem.Discipline.ToUpper()].Children.Add(_overlapGrid);
        }





        // background thread activity below
        private void RunBackgroundProgressBar(ProgressWindow progressWindow)
        {
            BackgroundWorker BackgroundThread = new BackgroundWorker();
            BackgroundThread.WorkerReportsProgress = true;
            BackgroundThread.DoWork += BackgroundThread_DoWork;
            BackgroundThread.RunWorkerCompleted += BackgroundThread_RunWorkerCompleted;

            BackgroundThread.RunWorkerAsync(progressWindow);
        }

        private void BackgroundThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            (e.Result as ProgressWindow).Close();
        }

        private void BackgroundThread_DoWork(object sender, DoWorkEventArgs e)
        {
            MongoClient MongoServer;
            List<Collab> total = new List<Collab>();
            do
            {
                List<string> temp = UserAddedContents.Keys.ToList();
                foreach (string key in temp)
                {
                    total.AddRange(UserAddedContents[key]);
                }
            } while (false);

            List<Collab> UserContent = total.FindAll(x => x.MainContent == true);

            List<BsonDocument> NewBson = ConvertToBson(UserContent);

            if (ProjectTabs.ServerConnected)
            {
                MongoServer = ProjectTabs.Mongo;
            }
            else
            {
                MongoServer = new MongoClient("mongodb://192.168.43.179:27017");
            }

            var ProjectDatabase = MongoServer.GetDatabase(_projectName);
            var collabentry = ProjectDatabase.GetCollection<BsonDocument>("UserEntry");

            ReplaceOptions replaceOptions = new ReplaceOptions() { IsUpsert = true };

            foreach (int value in DeletedID)
            {
                try
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("ContentID", value);
                    collabentry.DeleteOne(filter);
                }
                catch { }
            }

            int UserContentIndex = 0;
            foreach (BsonDocument value in NewBson)
            {
                var filter = Builders<BsonDocument>.Filter.Eq("ContentID", UserContent[UserContentIndex].Id);
                collabentry.ReplaceOne(filter, value, replaceOptions);
                UserContentIndex++;
            }

            DatabaseMisc misc = new DatabaseMisc() { ID = 0, LastAllocatedID = identity, LastDeletedList = DeletedID, ProjectStartDate = _startdatevalueMW.ToLongDateString() };
            var GetMisc = ProjectDatabase.GetCollection<DatabaseMisc>("DataBaseMisc");
            GetMisc.ReplaceOne(x => x.ID == 0, misc, replaceOptions);

            e.Result = e.Argument;
        }

    }
}
