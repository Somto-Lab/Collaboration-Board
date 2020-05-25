using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupedApp
{
    [BsonIgnoreExtraElements]
    class DatabaseMisc
    {
        public int ID { get; set; }
        public string ProjectStartDate { get; set; }
        public int LastAllocatedID { get; set; }
        public List<int> LastDeletedList { get; set; }
    }
}
