using Newtonsoft.Json;
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

        [JsonProperty("itemName")]
        public string ItemName { get; set; }

        [JsonProperty("isCompleted")]
        public bool IsCompleted { get; set; }
        public string CreatedDate { get; set; }
    }
}
