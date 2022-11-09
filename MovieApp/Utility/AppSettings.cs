using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Utility
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionString { get; set; }
        public TableStorage UserProfile { get; set; }
        public TableStorage Todo { get; set; }
    }
    public class ConnectionStrings
    {
        public string Storage { get; set; }
    }
    public class TableStorage
    {
        public string TableName { get; set; }
       
    }
}
