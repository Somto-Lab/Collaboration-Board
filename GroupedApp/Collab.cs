using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Drawing;
using MongoDB.Bson.Serialization.Attributes;

namespace GroupedApp
{
    public class Collab : IComparable, IEquatable<Collab>
    {
        public int Id { get; set; }
        public string Discipline { get; set; }
        public KeyValuePair<string,SolidColorBrush> DisciplineColor { get; set; }
        public string Content { get; set; }
        public bool MainContent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CompletedState { get; set; } // 1 = completed, 2 = failed, 3 = still active
        public int Row { get; set; }
        public int GroupedRow { get; set; }
        public bool MileStoneEntry { get; set; }
        //public List<int> Paired { get; set; } = new List<int>();

        public int CompareTo(object ob)
        {
            Collab _x = ob as Collab;
            if (_x == null)
                throw new NotImplementedException("Please check search");
            return Content.CompareTo(_x.Id);
        }

        public bool Equals(Collab here)
        {
            if (here == null) return false;
            return (this.Id.Equals(here.Id));
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override bool Equals(object here)
        {
            if (here == null) return false;
            Collab hereis = here as Collab;
            if (hereis == null) return false;
            else return Equals(hereis);
        }

        //public System.Windows.Media.Color DiscColor(object ob)
        //{
        //    Collab _x = ob as Collab;
        //    if (_x == null)
        //        throw new NotImplementedException("Please check search");
        //    return _x.Disciplinecolor;
        //}
    }
}
