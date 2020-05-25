using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.PeerToPeer.Collaboration;
using System.Runtime.InteropServices;
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

namespace GroupedApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UserControl
    {
        string _projectName;

        List<DateTime> _dates = new List<DateTime>();  DateTime _startdatevalueMW; //list to keep track of dates added to dategrid, get start date from projecttabs

        Dictionary<Grid, List<int>> GridRectangleRow = new Dictionary<Grid, List<int>>(); //dictionary to check rectangles added to grids
        int _lastColumnIndex = 0;

        readonly Dictionary<string, SolidColorBrush> ProjectColorSelection = new Dictionary<string, SolidColorBrush>(); //dictionary to pair color name and color

        Dictionary<string, string> DisciplineList = new Dictionary<string, string>(); //dictionary to pair disciplines and color name

        Dictionary<string, Grid> GridsAddedToProject = new Dictionary<string, Grid>(); //dictionary to pair disciplines and grid

        Dictionary<string, List<Collab>> UserAddedContents = new Dictionary<string, List<Collab>>(); //dictionary to pair discipline against content added

        int identity = 0x0000001; List<int> DeletedID = new List<int>(); int editedContentIdentifier = 0; //id of entries, list of deleted entries id, ID of content to be edited

        bool contentEdited = false; bool saving;

        List<int> MilestoneCheckGrid = new List<int>(); List<int> MilestoneIdentifier = new List<int>();

        int TBCGridCheck = 1;
        //
        //end of variable list
        //

        //start of methods
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void LoadNewProject()
        {
            _projectName = ProjectTabs.Project;
            _startdatevalueMW = ProjectTabs.StartDateValue;
            DatePopulate();
            AddColorsToProject();
        }

        private void DatePopulate()
        {
            string _formatdate;
            DateTime _tempStartDateValueMW = _startdatevalueMW.Date;

            for (int i = 0; i < DateGrid.ColumnDefinitions.Count; i++)
            {
                _dates.Add(_tempStartDateValueMW);
                _formatdate = _tempStartDateValueMW.ToLongDateString();
                TextBlock _dateblock = new TextBlock()
                {
                    FontStyle = FontStyles.Italic,
                    FontSize = 11,
                    FontWeight = FontWeights.Bold,
                    Background = Brushes.Yellow,
                    Text = _formatdate,
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    TextAlignment = TextAlignment.Center
                };
                Grid.SetColumn(_dateblock, i);
                _tempStartDateValueMW = _tempStartDateValueMW.AddDays(1);
                DateGrid.Children.Add(_dateblock);
            }
            RectangleAdd(DateGrid);
        }

        private void OpenPopup(object snd, RoutedEventArgs s)
        {
            UserContent content = new UserContent(_startdatevalueMW, DisciplineList, ProjectColorSelection);
            content.MileCheck = false;
            bool? _contentWindowResult = content.ShowDialog();
            if (_contentWindowResult.HasValue)
            {
                if ((bool)_contentWindowResult) AddUserContentToProject(content);
            }
        }

        private void AddUserContentToProject(UserContent UserInput)
        {
            bool _spanExist = UserInput._ReturnspanExist;
            
            CheckDisciplineExist(UserInput);

            int TempID;

            if (contentEdited)
            {
                TempID = editedContentIdentifier;
            }
            else
            {
                TempID = identity;
            }

            if (UserInput.ReturnStartDate.HasValue)
            {
                int[] index = CheckDatesExist(UserInput);

                if (_spanExist)
                {
                    List<Grid> grids = GridsAddedToProject.Values.ToList();
                    foreach (Grid grid in grids)
                    {
                        RectangleAdd(grid);
                    }
                    TimeSpan temp = (TimeSpan)(UserInput.ReturnEndDate - UserInput.ReturnStartDate); int temp2 = temp.Days + 1;

                    UserAddedContents[UserInput.ReturnDiscipline.ToUpper()].Add(new Collab() 
                    {   Id = TempID, 
                        Discipline = UserInput.ReturnDiscipline.ToUpper(), 
                        Content = UserInput.ReturnContent, 
                        StartDate = ((DateTime)UserInput.ReturnStartDate).Date, 
                        DisciplineColor = UserInput.ReturnDiscColor, 
                        Row = index[0], 
                        EndDate = ((DateTime)UserInput.ReturnEndDate).Date, 
                        MainContent = true, 
                        CompletedState = 3, 
                        MileStoneEntry = UserInput.MileCheck 
                    });

                    AddArrowsExtension(TempID, UserInput, index);

                    TextBlock _ContentText = new TextBlock()
                    {
                        Tag = TempID,
                        Text = UserInput.ReturnContent,
                        FontSize = 11,
                        Background = UserInput.ReturnDiscColor.Value
                    }; TextBlockStyle(_ContentText);
                    Grid.SetRow(_ContentText, index[0]); Grid.SetColumn(_ContentText, index[1]); Grid.SetColumnSpan(_ContentText, temp2);

                    Grid ContentGrid = GridsAddedToProject[UserInput.ReturnDiscipline.ToUpper()];
                    ContentGrid.Children.Add(_ContentText); AddOverlapGrid(index, UserInput, TempID); StateUpdate(4, 0);
                }
                else
                {
                    List<Grid> grids = GridsAddedToProject.Values.ToList();
                    foreach (Grid grid in grids)
                    {
                        RectangleAdd(grid);
                    }
                    UserAddedContents[UserInput.ReturnDiscipline.ToUpper()].Add(new Collab()
                    {
                        Id = TempID,
                        Discipline = UserInput.ReturnDiscipline.ToUpper(),
                        Content = UserInput.ReturnContent,
                        StartDate = ((DateTime)UserInput.ReturnStartDate).Date,
                        DisciplineColor = UserInput.ReturnDiscColor,
                        Row = index[0],
                        EndDate = ((DateTime)UserInput.ReturnEndDate).Date,
                        MainContent = true,
                        CompletedState = 3,
                        MileStoneEntry = UserInput.MileCheck
                    });
                    
                    TextBlock _ContentText = new TextBlock()
                    {
                        Tag = TempID,
                        Text = UserInput.ReturnContent,
                        Background = UserInput.ReturnDiscColor.Value
                    }; TextBlockStyle(_ContentText);
                    Grid.SetRow(_ContentText, index[0]); Grid.SetColumn(_ContentText, index[1]);

                    Grid ContentGrid = GridsAddedToProject[UserInput.ReturnDiscipline.ToUpper()];
                    ContentGrid.Children.Add(_ContentText); AddOverlapGrid(index, UserInput, TempID); StateUpdate(4, 0);
                }

                if (UserInput.MileCheck)
                    MilestoneFunction(UserInput.ReturnDiscColor, UserInput.ReturnContent, TempID, ((DateTime)UserInput.ReturnStartDate).ToLongDateString());
                else
                {
                    if (MilestoneIdentifier.Count != 0 && MilestoneIdentifier.Exists(x => x == TempID))
                        DeleteMilestone(TempID);
                }
            }

            if (!contentEdited)
                identity++;//increment id number for next
            contentEdited = false;
        }

        private void AddArrowsExtension(int _id, UserContent UserInput, int[] _Arrowindex)
        {
            DateTime _startdate = (DateTime)UserInput.ReturnStartDate; DateTime _enddate = (DateTime)UserInput.ReturnEndDate;
            int column = _Arrowindex[1];

            do
            {
                _startdate = _startdate.AddDays(1);

                UserAddedContents[UserInput.ReturnDiscipline.ToUpper()].Add(new Collab() { Id = _id, Row = _Arrowindex[0],
                    StartDate = _startdate, MainContent = false, Discipline = UserInput.ReturnDiscipline.ToUpper()});


                TextBlock _arrowTextblock = new TextBlock()
                {
                    Tag = _id,
                    Background = UserInput.ReturnDiscColor.Value
                }; TextBlockStyle(_arrowTextblock); Grid.SetRow(_arrowTextblock, _Arrowindex[0]); Grid.SetColumn(_arrowTextblock, ++column);

                Grid ContentGrid = GridsAddedToProject[UserInput.ReturnDiscipline.ToUpper()];
                ContentGrid.Children.Add(_arrowTextblock);

            } while (_startdate != _enddate);
        }

        private void AddOverlapGrid(int[] _index, UserContent UserInput, int _callerID)
        {

            TimeSpan _span = (TimeSpan)(UserInput.ReturnEndDate - UserInput.ReturnStartDate); int _spandays = _span.Days + 1;

            Grid _overlapGrid = new Grid
            {
                ShowGridLines = false,
                Tag = _callerID
            };
            RowDefinition row1 = new RowDefinition()
            {
                Height = new GridLength(35)
            }; _overlapGrid.RowDefinitions.Add(row1);
            RowDefinition row2 = new RowDefinition()
            {
                Height = new GridLength(35)
            }; _overlapGrid.RowDefinitions.Add(row2);
            ColumnDefinition column1 = new ColumnDefinition()
            {
                Width = new GridLength(42.5)
            }; _overlapGrid.ColumnDefinitions.Add(column1);
            ColumnDefinition column2 = new ColumnDefinition()
            {
                Width = new GridLength(42.5)
            }; _overlapGrid.ColumnDefinitions.Add(column2);

            Button _edit = new Button()
            {
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Tag = _callerID,
                ToolTip = "Edit",
                Content = new Image
                {
                    Width = 15,
                    Height = 15,
                    VerticalAlignment = VerticalAlignment.Center,
                    Source = new BitmapImage(new Uri("/Resources/edit.png", UriKind.Relative))
                }
            }; Grid.SetColumn(_edit, 0); Grid.SetRow(_edit, 0); _overlapGrid.Children.Add(_edit); _edit.Click += EditClick;

            Button _delete = new Button()
            {
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Tag = _callerID,
                ToolTip = "Delete",
                Content = new Image
                {
                    Width = 15,
                    Height = 15,
                    VerticalAlignment = VerticalAlignment.Center,
                    Source = new BitmapImage(new Uri("/Resources/delete.png", UriKind.Relative))
                }
            }; Grid.SetColumn(_delete, 1); Grid.SetRow(_delete, 0); _overlapGrid.Children.Add(_delete); _delete.Click += DeleteClick;

            Button _tick = new Button()
            {
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Tag = _callerID,
                ToolTip = "Completed",
                Content = new Image
                {
                    Width = 15,
                    Height = 15,
                    VerticalAlignment = VerticalAlignment.Center,
                    Source = new BitmapImage(new Uri("/Resources/tick.png", UriKind.Relative))
                }
            }; Grid.SetColumn(_tick, 0); Grid.SetRow(_tick, 1); _overlapGrid.Children.Add(_tick); _tick.Click += TickClick;

            Button _cross = new Button()
            {
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Tag = _callerID,
                ToolTip = "Missed",
                Content = new Image
                {
                    Width = 15,
                    Height = 15,
                    VerticalAlignment = VerticalAlignment.Center,
                    Source = new BitmapImage(new Uri("/Resources/cross.png", UriKind.Relative))
                }
            }; Grid.SetColumn(_cross, 1); Grid.SetRow(_cross, 1); _overlapGrid.Children.Add(_cross); _cross.Click += CrossClick;

            _overlapGrid.Background = Brushes.Transparent;
            Grid.SetRow(_overlapGrid, _index[0]); Grid.SetColumn(_overlapGrid, _index[1]); Grid.SetColumnSpan(_overlapGrid, _spandays);
            Grid ContentGrid = GridsAddedToProject[UserInput.ReturnDiscipline.ToUpper()];
            ContentGrid.Children.Add(_overlapGrid);
            _overlapGrid.Style = (Style)FindResource("OverlapStyle");
        }

        private void CheckDisciplineExist(UserContent UserInput)
        {
            //check if discipline has already been added

            string _disc2 = UserInput.ReturnDiscipline.ToUpper();
            KeyValuePair<string, SolidColorBrush> _discColor = UserInput.ReturnDiscColor; 

            if (!UserAddedContents.ContainsKey(_disc2))
            {
                ColumnDefinition column = new ColumnDefinition
                {
                    Width = new GridLength(100)
                }; Groupgrid.ColumnDefinitions.Add(column);

                int b = Groupgrid.ColumnDefinitions.Count;
                DisciplineColorChange discChange = new DisciplineColorChange(_disc2, _discColor, ProjectColorSelection);
                discChange.ChangingColor += BoundDisciplineBackground;
                discChange.ID = --b;
                Grid.SetColumn(discChange, b);

                Groupgrid.Children.Add(discChange);

                //create new discipline grid in stackpanel
                Grid newDisciplineGrid = new Grid()
                {
                    Margin = new Thickness(70, 0, 0, 0),
                    Tag = b
                };
                RowDefinition rowdef = new RowDefinition
                {
                    Height = new GridLength(70)
                }; newDisciplineGrid.RowDefinitions.Add(rowdef);

                for(int i = 0; i < DateGrid.ColumnDefinitions.Count; i++)
                {
                    ColumnDefinition columndef = new ColumnDefinition
                    {
                        Width = new GridLength(85)
                    }; newDisciplineGrid.ColumnDefinitions.Add(columndef);
                }
                GridsStack.Children.Add(newDisciplineGrid);

                RectangleAdd(newDisciplineGrid);

                GridsAddedToProject.Add(_disc2, newDisciplineGrid);
                DisciplineList.Add(_disc2, _discColor.Key);
                UserAddedContents.Add(_disc2, new List<Collab>());
            }
        }

        private int[] CheckDatesExist(UserContent UserInput)
        {
            int[] index = new int[2]; DateTime _EndDate = ((DateTime)UserInput.ReturnEndDate).Date; _lastColumnIndex = _dates.Count; DateTime _dt2 = ((DateTime)UserInput.ReturnStartDate).Date;

            if (_dates.Exists(x => x == _EndDate))
            {
                index = SearchDate(UserInput.ReturnDiscipline.ToUpper(), _dt2, _EndDate);
            }
            else
            {
                string _formatdate;
                DateTime _LastDate = _dates[_dates.Count - 1]; int i = _dates.Count;
                do
                {
                    _LastDate = _LastDate.AddDays(1);
                    _dates.Add(_LastDate);
                    ColumnDefinition _column = new ColumnDefinition
                    {
                        Width = new GridLength(85)
                    }; DateGrid.ColumnDefinitions.Add(_column);
                    List<Grid> tempgrids = GridsAddedToProject.Values.ToList();
                    foreach (Grid grid in tempgrids)
                    {
                        ColumnDefinition _col = new ColumnDefinition
                        {
                            Width = new GridLength(85)
                        };
                        grid.ColumnDefinitions.Add(_col);
                    }


                    _formatdate = _LastDate.ToLongDateString();
                    TextBlock _dateblock = new TextBlock()
                    {
                        FontStyle = FontStyles.Italic,
                        FontSize = 11,
                        FontWeight = FontWeights.Bold,
                        Text = _formatdate,
                        Background = Brushes.Yellow,
                        TextWrapping = TextWrapping.WrapWithOverflow,
                        TextAlignment = TextAlignment.Center
                    };

                    Grid.SetColumn(_dateblock, i);
                    DateGrid.Children.Add(_dateblock);
                    i++;
                } while (_LastDate != _EndDate);
                index = SearchDate(UserInput.ReturnDiscipline.ToUpper(), _dt2, _EndDate);
                RectangleAdd(DateGrid);
            }
            return index;            
        }

        private int[] SearchDate(string DisciplineText, DateTime ContentStartDate, DateTime ContentEndDate)
        {
            TimeSpan _span = ContentEndDate - ContentStartDate; int[] bt = new int[2];
            int _totalDays = 0; DateTime ModifiedContentStartDate = ContentStartDate;

            int col = _dates.FindIndex(_xx => _xx.Equals(ContentStartDate));

            Grid ContentGrid = GridsAddedToProject[DisciplineText];

            for (int i = 0; i < ContentGrid.RowDefinitions.Count; i++)
            {
                if (i == ContentGrid.RowDefinitions.Count - 1)
                {
                    RowDefinition row = new RowDefinition
                    {
                        Height = new GridLength(70)
                    }; ContentGrid.RowDefinitions.Add(row);
                }
                List<Collab> temp = UserAddedContents[DisciplineText];              //potentially move outside loop

                List<Collab> DisciplineResults = temp.FindAll(x => x.StartDate == ModifiedContentStartDate);

                if (!DisciplineResults.Exists(x => x.Row == i))
                {
                    if (_totalDays == _span.Days)
                    {
                        bt = new int[] { i, col };
                        break;
                    }
                    _totalDays++;
                    i--; ModifiedContentStartDate = ModifiedContentStartDate.AddDays(1);
                }
                else
                {
                    ModifiedContentStartDate = ContentStartDate; _totalDays = 0;
                }
            }
            return bt;
        }

        private void EditClick(object snd, RoutedEventArgs s)
        {
            Button button = (Button)snd;
            int _callingID = (int)button.Tag;
            List<Collab> total = new List<Collab>();
            do
            {
                List<string> temp = UserAddedContents.Keys.ToList();
                foreach (string key in temp)
                {
                    total.AddRange(UserAddedContents[key]);
                }
            } while (false);

            int _listFirstIndex = total.FindIndex(x => x.Id == _callingID);
            Collab _editItem = total[_listFirstIndex];
            UserContent content = new UserContent(_startdatevalueMW, _editItem, DisciplineList, ProjectColorSelection);
            if (_editItem.MileStoneEntry)
            {
                content.MilestoneCheckbox.IsChecked = true; content.MileCheck = true;
            }
            else
            {
                content.MilestoneCheckbox.IsChecked = false; content.MileCheck = false;
            }
            bool? _contentWindowResult = content.ShowDialog();
            TextBlock _objecttodelete = new TextBlock(); Grid _overlapptodelete = new Grid();

            if ((bool)_contentWindowResult)
            {
                for (int j = 0; j < GridsAddedToProject[_editItem.Discipline].Children.Count; j++)
                {
                    var _tempobject = GridsAddedToProject[_editItem.Discipline].Children[j];
                    _objecttodelete = _tempobject as TextBlock;
                    _overlapptodelete = _tempobject as Grid;
                    if (_objecttodelete != null)
                    {
                        if (_objecttodelete.Tag != null && (int)_objecttodelete.Tag == _callingID)
                        {
                            GridsAddedToProject[_editItem.Discipline].Children.RemoveAt(j);
                            j--;
                        }
                    }
                    else if (_overlapptodelete != null)
                    {
                        if (_overlapptodelete.Tag != null && (int)_overlapptodelete.Tag == _callingID)
                        {
                            GridsAddedToProject[_editItem.Discipline].Children.RemoveAt(j);
                            j--;
                        }
                    }
                }
                int indexedCollab = UserAddedContents[_editItem.Discipline].FindIndex(x => x.Id == _callingID);
                do
                {
                    UserAddedContents[_editItem.Discipline].RemoveAt(indexedCollab);
                    _listFirstIndex = UserAddedContents[_editItem.Discipline].FindIndex(x => x.Id == _callingID);
                } while (_listFirstIndex != -1);

                contentEdited = true; editedContentIdentifier = _callingID;
                AddUserContentToProject(content);
                //DeleteEditReinstateMilestone(2, _callingID);  not required as edit mode calls back AddUserContentToProject which updates milestone
            }
        }

        private void DeleteClick(object snd, RoutedEventArgs s)
        {
            Button button = (Button)snd;
            int _callingID = (int)button.Tag;
            List<Collab> total = new List<Collab>();
            do
            {
                List<string> temp = UserAddedContents.Keys.ToList();
                foreach (string key in temp)
                {
                    total.AddRange(UserAddedContents[key]);
                }
            } while (false);

            int _listFirstIndex = total.FindIndex(x => x.Id == _callingID);
            Collab _deleteItem = total[_listFirstIndex];
            TextBlock _objecttodelete = new TextBlock(); Grid _overlapptodelete = new Grid();
            ////////check if actually a milestone before delete
            DeleteMilestone(_callingID);

            for (int j = 0; j < GridsAddedToProject[_deleteItem.Discipline].Children.Count; j++)
            {
                var _tempobject = GridsAddedToProject[_deleteItem.Discipline].Children[j];
                _objecttodelete = _tempobject as TextBlock;
                _overlapptodelete = _tempobject as Grid;
                if (_objecttodelete != null)
                {
                    if (_objecttodelete.Tag != null && (int)_objecttodelete.Tag == _callingID)
                    {
                        GridsAddedToProject[_deleteItem.Discipline].Children.RemoveAt(j);
                        j--;
                    }
                }
                else if (_overlapptodelete != null)
                {
                    if (_overlapptodelete.Tag != null && (int)_overlapptodelete.Tag == _callingID)
                    {
                        GridsAddedToProject[_deleteItem.Discipline].Children.RemoveAt(j);
                        j--;
                    }
                }
            }
            int indexedCollab = UserAddedContents[_deleteItem.Discipline].FindIndex(x => x.Id == _callingID);
            do
            {
                UserAddedContents[_deleteItem.Discipline].RemoveAt(indexedCollab);
                _listFirstIndex = UserAddedContents[_deleteItem.Discipline].FindIndex(x => x.Id == _callingID);
            } while (_listFirstIndex != -1);
            StateUpdate(4, 0);
            DeletedID.Add(_callingID);
        }

        private void TickClick(object snd, RoutedEventArgs s)
        {
            Button button = (Button)snd;
            int _callingID = (int)button.Tag;

            StateUpdate(1, _callingID);

            List<Collab> total = new List<Collab>();
            do
            {
                List<string> temp = UserAddedContents.Keys.ToList();
                foreach (string key in temp)
                {
                    total.AddRange(UserAddedContents[key]);
                }
            } while (false);

            int _listFirstIndex = total.FindIndex(x => x.Id == _callingID);
            Collab _editItem = total[_listFirstIndex];

            Grid _overlapGrid = new Grid();

            for (int i = 0; i < GridsAddedToProject[_editItem.Discipline].Children.Count; i++)
            {
                var temp = GridsAddedToProject[_editItem.Discipline].Children[i];
                _overlapGrid = temp as Grid;

                if (_overlapGrid != null)
                {
                    if (_overlapGrid.Tag != null && (int)_overlapGrid.Tag == _callingID)
                    {
                        GridsAddedToProject[_editItem.Discipline].Children.RemoveAt(i);
                        break;
                    }
                }
            }
            
            if(_overlapGrid != null)
            {
                _overlapGrid.Children.Clear();
                _overlapGrid.ColumnDefinitions.Clear(); _overlapGrid.RowDefinitions.Clear();
            }
            else
            {
                _overlapGrid = new Grid();
            }

            RowDefinition row = new RowDefinition(); _overlapGrid.RowDefinitions.Add(row);
            ColumnDefinition column = new ColumnDefinition(); _overlapGrid.ColumnDefinitions.Add(column);
            _overlapGrid.Tag = _callingID;

            Button undo = new Button
            {
                Tag = _callingID,
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
            _overlapGrid.Style = (Style)FindResource("UndoStyle");
            GridsAddedToProject[_editItem.Discipline].Children.Add(_overlapGrid);
        }

        private void CrossClick(object snd, RoutedEventArgs s)
        {
            Button button = (Button)snd;
            int _callingID = (int)button.Tag;

            StateUpdate(2, _callingID);

            List<Collab> total = new List<Collab>();
            do
            {
                List<string> temp = UserAddedContents.Keys.ToList();
                foreach (string key in temp)
                {
                    total.AddRange(UserAddedContents[key]);
                }
            } while (false);

            int _listFirstIndex = total.FindIndex(x => x.Id == _callingID);
            Collab _editItem = total[_listFirstIndex];

            Grid _overlapGrid = new Grid();

            for (int i = 0; i < GridsAddedToProject[_editItem.Discipline].Children.Count; i++)
            {
                var temp = GridsAddedToProject[_editItem.Discipline].Children[i];
                _overlapGrid = temp as Grid;

                if (_overlapGrid != null)
                {
                    if (_overlapGrid.Tag != null && (int)_overlapGrid.Tag == _callingID)
                    {
                        GridsAddedToProject[_editItem.Discipline].Children.RemoveAt(i);
                        break;
                    }
                }
            }

            if (_overlapGrid != null)
            {
                _overlapGrid.Children.Clear();
                _overlapGrid.ColumnDefinitions.Clear(); _overlapGrid.RowDefinitions.Clear();
            }
            else
            {
                _overlapGrid = new Grid();
            }

            RowDefinition row = new RowDefinition(); _overlapGrid.RowDefinitions.Add(row);
            ColumnDefinition column = new ColumnDefinition(); _overlapGrid.ColumnDefinitions.Add(column);
            _overlapGrid.Tag = _callingID;


            Button undo = new Button
            {
                Tag = _callingID,
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
            _overlapGrid.Style = (Style)FindResource("UndoStyle");
            GridsAddedToProject[_editItem.Discipline].Children.Add(_overlapGrid);
        }

        private void UndoClick(object snd, RoutedEventArgs s)
        {
            Button button = (Button)snd;
            int _callingID = (int)button.Tag;

            StateUpdate(3, _callingID);

            List<Collab> total = new List<Collab>();
            do
            {
                List<string> temp = UserAddedContents.Keys.ToList();
                foreach (string key in temp)
                {
                    total.AddRange(UserAddedContents[key]);
                }
            } while (false);

            int _listFirstIndex = total.FindIndex(x => x.Id == _callingID);
            Collab _editItem = total[_listFirstIndex];

            Grid _overlapGrid = new Grid();

            for (int i = 0; i < GridsAddedToProject[_editItem.Discipline].Children.Count; i++)
            {
                var temp = GridsAddedToProject[_editItem.Discipline].Children[i];
                _overlapGrid = temp as Grid;

                if (_overlapGrid != null)
                {
                    if (_overlapGrid.Tag != null && (int)_overlapGrid.Tag == _callingID)
                    {
                        GridsAddedToProject[_editItem.Discipline].Children.RemoveAt(i);
                        break;
                    }
                }
            }
            _overlapGrid.Children.Clear();
            _overlapGrid.ColumnDefinitions.Clear(); _overlapGrid.RowDefinitions.Clear();

            RowDefinition row1 = new RowDefinition()
            {
                Height = new GridLength(35)
            }; _overlapGrid.RowDefinitions.Add(row1);
            RowDefinition row2 = new RowDefinition()
            {
                Height = new GridLength(35)
            }; _overlapGrid.RowDefinitions.Add(row2);
            ColumnDefinition column1 = new ColumnDefinition()
            {
                Width = new GridLength(42.5)
            }; _overlapGrid.ColumnDefinitions.Add(column1);
            ColumnDefinition column2 = new ColumnDefinition()
            {
                Width = new GridLength(42.5)
            }; _overlapGrid.ColumnDefinitions.Add(column2);

            Button _edit = new Button()
            {
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Tag = _callingID,
                Content = new Image
                {
                    Width = 15,
                    Height = 15,
                    VerticalAlignment = VerticalAlignment.Center,
                    Source = new BitmapImage(new Uri("/Resources/edit.png", UriKind.Relative))
                }
            }; Grid.SetColumn(_edit, 0); Grid.SetRow(_edit, 0); _overlapGrid.Children.Add(_edit); _edit.Click += EditClick;

            Button _delete = new Button()
            {
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Tag = _callingID,
                Content = new Image
                {
                    Width = 15,
                    Height = 15,
                    VerticalAlignment = VerticalAlignment.Center,
                    Source = new BitmapImage(new Uri("/Resources/delete.png", UriKind.Relative))
                }
            }; Grid.SetColumn(_delete, 1); Grid.SetRow(_delete, 0); _overlapGrid.Children.Add(_delete); _delete.Click += DeleteClick;

            Button _tick = new Button()
            {
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Tag = _callingID,
                Content = new Image
                {
                    Width = 15,
                    Height = 15,
                    VerticalAlignment = VerticalAlignment.Center,
                    Source = new BitmapImage(new Uri("/Resources/tick.png", UriKind.Relative))
                }
            }; Grid.SetColumn(_tick, 0); Grid.SetRow(_tick, 1); _overlapGrid.Children.Add(_tick); _tick.Click += TickClick;

            Button _cross = new Button()
            {
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Tag = _callingID,
                Content = new Image
                {
                    Width = 15,
                    Height = 15,
                    VerticalAlignment = VerticalAlignment.Center,
                    Source = new BitmapImage(new Uri("/Resources/cross.png", UriKind.Relative))
                }
            }; Grid.SetColumn(_cross, 1); Grid.SetRow(_cross, 1); _overlapGrid.Children.Add(_cross); _cross.Click += CrossClick;

            _overlapGrid.Style = (Style)FindResource("OverlapStyle");
            GridsAddedToProject[_editItem.Discipline].Children.Add(_overlapGrid);
            //DeleteMilestone(_callingID);
            DeletedID.Remove(_callingID);
        }

        private void MilestoneFunction(KeyValuePair<string,SolidColorBrush> disciplineColor, string content, int _callingID, string _formatdate)
        {
            if (MilestoneIdentifier.Count != 0 && MilestoneIdentifier.Exists(x => x == _callingID))
            {
                DeleteMilestone(_callingID);
            }

            int Column; int Row;
            if (MilestoneCheckGrid.Count == 0)
            {
                Column = 1;
                Row = MilestoneCheckGrid.Count;
            }
            else
            {
                Column = MilestoneCheckGrid.Last();
                Row = MilestoneGrid.RowDefinitions.Count - 1;
            }

            TextBlock MileTextBlock = new TextBlock
            {
                Tag = _callingID,
                Text = content,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 70,
                FontSize = 16,
                FontStyle = FontStyles.Oblique,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.WrapWithOverflow,
                Background = disciplineColor.Value
            };

            TextBlock MileTextBlockDate = new TextBlock
            {
                Tag = _callingID,
                FontStyle = FontStyles.Italic,
                Height = 30,
                VerticalAlignment = VerticalAlignment.Bottom,
                FontWeight = FontWeights.Bold,
                Background = Brushes.Yellow,
                Text = _formatdate,
                TextWrapping = TextWrapping.WrapWithOverflow,
                TextAlignment = TextAlignment.Center
            };

            if (Column == 4)
            {
                Grid.SetColumn(MileTextBlock, 3); Grid.SetRow(MileTextBlock, Row);
                Grid.SetColumn(MileTextBlockDate, 3); Grid.SetRow(MileTextBlockDate, Row);
                MilestoneCheckGrid.Add(1);
                RowDefinition row1 = new RowDefinition()
                {
                    Height = new GridLength(100)
                }; MilestoneGrid.RowDefinitions.Add(row1);
            }
            else
            {
                Grid.SetColumn(MileTextBlock, 1); Grid.SetRow(MileTextBlock, Row);
                Grid.SetColumn(MileTextBlockDate, 1); Grid.SetRow(MileTextBlockDate, Row);
                if (MilestoneCheckGrid.Count == 0)
                {
                    MilestoneCheckGrid.Add(4);
                }
                else
                {
                    MilestoneCheckGrid[Row - 1] = 4;
                }
            }

            MilestoneGrid.Children.Add(MileTextBlock);
            MilestoneGrid.Children.Add(MileTextBlockDate);
            MilestoneIdentifier.Add(_callingID);

        }

        private void DeleteMilestone(int _callingID)
        {
            List<TextBlock> MilestoneContent = new List<TextBlock>();

            for (int j = 0; j < MilestoneGrid.Children.Count; j++)
            {
                var _tempobject = MilestoneGrid.Children[j];
                TextBlock _objecttodelete = _tempobject as TextBlock;

                if (_objecttodelete != null)
                {
                    if ((int)_objecttodelete.Tag == _callingID)
                    {
                        MilestoneGrid.Children.RemoveAt(j);
                        j--;
                    }
                    else
                    {
                        MilestoneContent.Add(_objecttodelete);
                    }
                }
            }

            MilestoneGrid.Children.Clear();

            MilestoneCheckGrid.Clear(); MilestoneGrid.RowDefinitions.Clear();
            RowDefinition row1 = new RowDefinition()
            {
                Height = new GridLength(100)
            }; MilestoneGrid.RowDefinitions.Add(row1);

            for (int i = 0; i < MilestoneContent.Count; i++)
            {
                TextBlock RealText = MilestoneContent[i];
                TextBlock block = new TextBlock(); string _text = "";
                if (RealText.Background == Brushes.Yellow)
                {
                    _text = RealText.Text;
                    for (int j = ++i; j < MilestoneContent.Count; j++)
                    {
                        TextBlock temp = MilestoneContent[j];
                        if (temp.Tag == RealText.Tag)
                        {
                            block = MilestoneContent[j];
                        }
                    }
                }
                else
                {
                    block = MilestoneContent[i]; i++;
                    _text = MilestoneContent[i].Text; i--;
                }

                KeyValuePair<string, SolidColorBrush> tempColor = new KeyValuePair<string, SolidColorBrush>("temp", (SolidColorBrush)block.Background);
                MilestoneFunction(tempColor, block.Text, (int)block.Tag, _text);
                i++;
            }
            if (MilestoneIdentifier.Exists(x => x == _callingID))
            {
                int index = MilestoneIdentifier.FindIndex(x => x == _callingID);
                MilestoneIdentifier.RemoveAt(index);
            }
        }

        private void StateUpdate(int state, int _callerID)
        {
            List<Collab> total = new List<Collab>();
            List<string> temp = UserAddedContents.Keys.ToList();
            foreach (string key in temp)
            {
                total.AddRange(UserAddedContents[key]);
            }

            if (state != 4)
            {
                Collab collab = total.Find(x => x.Id == _callerID);      //returns a pointer
                collab.CompletedState = state;
            }         
                
            List<Collab> EditedTotal =  total.FindAll(x => x.MainContent == true);
            TotalBlock.Text = "" + EditedTotal.Count;
            if (total.Exists(x => x.CompletedState == 1))
            {
                List<Collab> complete = total.FindAll(x => x.CompletedState == 1);
                double f = complete.Count; double y = EditedTotal.Count;
                double percentage = f / y * 100;
                CompleteBlock.Text = string.Format("{0:0}%", percentage);
            }
            else
            {
                CompleteBlock.Text = string.Format("{0:0}%", 0);
            }
            if (total.Exists(x => x.CompletedState == 2))
            {
                List<Collab> missed = total.FindAll(x => x.CompletedState == 2);
                double f = missed.Count; double y = EditedTotal.Count;
                double percentage = f / y * 100;
                MissedBlock.Text = string.Format("{0:0}%", percentage);
            }
            else
            {
                MissedBlock.Text = string.Format("{0:0}%", 0);
            }
        }

        private void PDFView(object snd, RoutedEventArgs s)
        {
            PDFViewer PDF = new PDFViewer();
            PDF.Owner = Window.GetWindow(this);
            PDF.Show();
        }

        public void BoundDisciplineBackground(object snd, DisciplineChange change)
        {
            if (!change.DeleteDiscipline)
            {
                if (!string.Equals(change.OldDisciplineName, change.NewDiscipline))
                {
                    DisciplineList.Remove(change.OldDisciplineName, out string value1);
                    GridsAddedToProject.Remove(change.OldDisciplineName, out Grid value2);
                    UserAddedContents.Remove(change.OldDisciplineName, out List<Collab> value3);

                    DisciplineList[change.NewDiscipline] = value1;
                    GridsAddedToProject[change.NewDiscipline] = value2;
                    UserAddedContents[change.NewDiscipline] = value3;
                }
                if (change.NewColorName != DisciplineList[change.NewDiscipline])
                {
                    for (int i = 0; i < GridsAddedToProject[change.NewDiscipline].Children.Count; i++)
                    {
                        var tempobject = GridsAddedToProject[change.NewDiscipline].Children[i];
                        TextBlock textBlock = tempobject as TextBlock;
                        if (textBlock != null)
                        {
                            textBlock.Background = change.NewColor;
                        }
                    }
                    DisciplineList[change.NewDiscipline] = change.NewColorName;
                    foreach (Collab collab in UserAddedContents[change.NewDiscipline])
                    {
                        collab.DisciplineColor = new KeyValuePair<string, SolidColorBrush>(change.NewColorName, change.NewColor);
                    }
                }
            }
            else
            {
                DisciplineList.Remove(change.OldDisciplineName);
                GridsAddedToProject.Remove(change.OldDisciplineName, out Grid value);
                GridRectangleRow.Remove(value);
                UserAddedContents.Remove(change.OldDisciplineName, out List<Collab> value1);

                List<Collab> value2 = value1.FindAll(x => x.MainContent == true);
                foreach(Collab collab in value2)
                {
                    DeleteMilestone(collab.Id);
                    DeletedID.Add(collab.Id);
                }

                bool DeleteHappened = false;
                for (int i = 0; i < Groupgrid.Children.Count; i++)
                {
                    var tempobject = Groupgrid.Children[i]; 
                    DisciplineColorChange discipline = tempobject as DisciplineColorChange;
                    if (discipline != null)
                    {
                        if (DeleteHappened)
                        {
                            int tempID = discipline.ID - 1;
                            Grid.SetColumn(discipline, tempID);
                        }
                        if (discipline.ID == change.Identity)
                        {
                            Groupgrid.Children.RemoveAt(i); i--;
                            Groupgrid.ColumnDefinitions.RemoveAt(change.Identity);
                            DeleteHappened = true;
                        }
                        --discipline.ID;
                    }
                }

                for (int i = 0; i < GridsStack.Children.Count; i++)
                {
                    Grid DisciplineGridToDelete = (Grid)GridsStack.Children[i];
                    if (DisciplineGridToDelete.Tag != null)
                    {
                        if ((int)DisciplineGridToDelete.Tag == change.Identity)
                        {
                            GridsStack.Children.RemoveAt(i);
                        }
                    }
                }
                StateUpdate(4, 0);
            }
        }

        public void ServerConnect(object snd, RoutedEventArgs s)
        {
            bool ConnectionAlive = CheckServerConnection("192.168.43.179");
            if (ConnectionAlive)
            {
                saving = true;
                ProgressWindow progressWindow = new ProgressWindow();
                progressWindow.Show();
                RunBackgroundProgressBar(progressWindow);
            }
            else
            {
                MessageBox.Show("Unable to save, please check your network connection. Alternatively " +
                    "the server might be down, please try again later", "Connecting to server", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private List<BsonDocument> ConvertToBson (List<Collab> EntryCollabList)
        {
            List<BsonDocument> ReturningList = new List<BsonDocument>();

            foreach(Collab TempValue in EntryCollabList)
            {
                BsonDocument bsonElements = new BsonDocument
                {
                    {"ContentID", TempValue.Id },
                    {"ContentDiscipline", TempValue.Discipline },
                    {"ContentColor", TempValue.DisciplineColor.Key },
                    {"Content", TempValue.Content },
                    {"ContentStartDate", TempValue.StartDate.ToLongDateString() },
                    {"ContentEndDate", TempValue.EndDate.ToLongDateString() },
                    {"ContentCompletedState", TempValue.CompletedState },
                    {"ContentMilestone", TempValue.MileStoneEntry }
                };
                ReturningList.Add(bsonElements);
            }

            return ReturningList;
        }

        private void RectangleAdd(Grid AskingGrid)
        {
            bool _newRow = false; List<int> _rectangleRow;
            for (int a = 0; a < AskingGrid.RowDefinitions.Count; a++)
            {
                if (GridRectangleRow.TryGetValue(AskingGrid, out _rectangleRow))
                {

                }
                else
                {
                    _rectangleRow = new List<int>();
                    GridRectangleRow.Add(AskingGrid, _rectangleRow);
                }

                if (!_rectangleRow.Contains(a))
                {
                    _rectangleRow.Add(a);
                    _newRow = true;
                }
                bool _columnKey = true;
                for (int j = 0; j < AskingGrid.ColumnDefinitions.Count; j++)
                {
                    if (_newRow)
                    {
                        Rectangle rectangle = new Rectangle
                        {
                            Stroke = Brushes.Black,
                            StrokeThickness = 1
                        }; Grid.SetRow(rectangle, a); Grid.SetColumn(rectangle, j); AskingGrid.Children.Add(rectangle);
                    }
                    else
                    {
                        if (_columnKey)
                        {
                            j = _lastColumnIndex; _columnKey = false;
                        }
                        Rectangle rectangle = new Rectangle
                        {
                            Stroke = Brushes.Black,
                            StrokeThickness = 1
                        }; Grid.SetRow(rectangle, a); Grid.SetColumn(rectangle, j); AskingGrid.Children.Add(rectangle);
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

        private static void TextBlockStyle(TextBlock _textblock)
        {
            _textblock.TextWrapping = TextWrapping.WrapWithOverflow;
            _textblock.TextAlignment = TextAlignment.Center;
            _textblock.FontSize = 14;
            _textblock.FontStyle = FontStyles.Oblique;
            _textblock.Height = 65;
            //_textblock.FontFamily
        }

        private void AddColorsToProject()
        {
            ProjectColorSelection.Add("AliceBlue", Brushes.AliceBlue);
            ProjectColorSelection.Add("Aqua", Brushes.Aqua);
            ProjectColorSelection.Add("Blue", Brushes.Blue);
            ProjectColorSelection.Add("Brown", Brushes.Brown);
            ProjectColorSelection.Add("Coral", Brushes.Coral);
            ProjectColorSelection.Add("Green", Brushes.Green);
            ProjectColorSelection.Add("Orange", Brushes.Orange);
            ProjectColorSelection.Add("Pink", Brushes.Pink);
            ProjectColorSelection.Add("Red", Brushes.Red);
        }
    }
}
