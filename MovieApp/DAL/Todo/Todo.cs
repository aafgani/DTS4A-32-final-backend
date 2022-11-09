using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DAL.Todo
{
    public class Todo
    {
        public string Id { get; set; }
        public string ItemName { get; set; }
        public bool IsCompleted { get; set; }
        public string CreatedDate { get; set; }
    }
}
