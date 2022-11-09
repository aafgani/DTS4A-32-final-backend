using MovieApp.DAL.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Storage.Interface
{
    public interface ITodoTableClient
    {
        void Upsert(string RowKey, Todo Todo);
        Todo GetByKey(string RowKey);
        List<Todo> GetAll();
        void DeleteByKey(string RowKey);
    }
}
