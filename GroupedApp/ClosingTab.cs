using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

//
//
// code gotten from https://www.codeproject.com/articles/84213/how-to-add-a-close-button-to-a-wpf-tabitem with slight
// modifications mine
//
//

namespace GroupedApp
{
    class ClosingTab : TabItem
    {
        public ClosingTab()
        {
            ClosingHeader closeheader = new ClosingHeader();
            Header = closeheader;

            //closeheader.button_change.MouseEnter += new MouseEventHandler(Button_change_MouseEnter);
            //closeheader.button_change.MouseLeave += new MouseEventHandler(Button_change_MouseLeave);
            closeheader.button_change.Click += new RoutedEventHandler(Button_change_clicked);
            closeheader.label_TabTitle.SizeChanged += new SizeChangedEventHandler(Label_title_SizeChanged);
        }

        public string Title
        {
            get { return (string)((ClosingHeader)Header).label_TabTitle.Content; }
            set
            {
                ((ClosingHeader)Header).label_TabTitle.Content = value;
            }
        }

        protected override void OnSelected(RoutedEventArgs e)
        {
            base.OnSelected(e);
            ((ClosingHeader)Header).button_change.Visibility = Visibility.Visible;
        }

        protected override void OnUnselected(RoutedEventArgs e)
        {
            base.OnUnselected(e);
            ((ClosingHeader)Header).button_change.Visibility = Visibility.Hidden;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            ((ClosingHeader)Header).button_change.Visibility = Visibility.Visible;
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            ((ClosingHeader)Header).button_change.Visibility = Visibility.Hidden;
        }

        //below is useful if trying to change content colour
        //void Button_change_MouseEnter (object snd, MouseEventArgs e)
        //{
        //    ((ClosingHeader)Header).button_change.Foreground = Brushes.Red;
        //}

        //void Button_change_MouseLeave (object snd, MouseEventArgs e)
        //{
        //    ((ClosingHeader)Header).button_change.Foreground = Brushes.Black;
        //}

        void Button_change_clicked (object snd, RoutedEventArgs s)
        {
            ModifyProjectName LaunchEdit = new ModifyProjectName(Title);
            bool? result = LaunchEdit.ShowDialog();
            if ((bool)result)
            {
                if (LaunchEdit.ConfirmProject)
                {
                    Title = LaunchEdit.ProjectNameEntry.Text;
                }else if (LaunchEdit.DeleteProject)
                {
                    ((TabControl)Parent).Items.Remove(this);
                }
            }            
        }

        void Label_title_SizeChanged (object snd, SizeChangedEventArgs a)
        {
            ((ClosingHeader)Header).button_change.Margin = new Thickness(((ClosingHeader)Header).label_TabTitle.ActualWidth + 5, 3, 4, 0);
        }
        
    }
}
