using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Options;
using MovieApp.DAL.Todo;
using MovieApp.Service.Storage.Interface;
using MovieApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Storage
{
    public class TodoTableClient : ITodoTableClient
    {
        private TableClient tableClient;
        private string tableName;

        public TodoTableClient(IOptions<AppSettings> options)
        {
            var config = options.Value;
            tableName = config.Todo.TableName;
            tableClient = new TableClient(config.ConnectionString.Storage, tableName);
            tableClient.CreateIfNotExists();
        }

        public void DeleteByKey(string RowKey)
        {
            tableClient.DeleteEntity(tableName, RowKey);
        }

        public List<Todo> GetAll()
        {
            var todos = new List<Todo>();
            string filter = $"PartitionKey eq '{tableName}' ";
            Pageable<TableEntity> entities = tableClient.Query<TableEntity>(filter: filter);

            foreach (TableEntity entity in entities)
            {
                var key = entity.GetString("RowKey");
                if (!string.IsNullOrEmpty(key))
                {
                    todos.Add(new Todo
                    {
                        Id = key,
                        ItemName = entity.GetString("ItemName"),
                        IsCompleted = entity.GetBoolean("IsCompleted").Value,
                        CreatedDate = entity.GetDateTime("Timestamp")?.ToString("dd MMMM yyyy HH:mm:ss")
                    });
                }
            }

            return todos;
        }

        public Todo GetByKey(string RowKey)
        {
            var todo = new Todo();
            string filter = $"PartitionKey eq '{tableName}' or RowKey eq '{RowKey}' ";
            Pageable<TableEntity> entities = tableClient.Query<TableEntity>(filter: filter);
            foreach (TableEntity entity in entities)
            {
                var key = entity.GetString("RowKey");
                if (key.Equals(RowKey))
                {
                    todo.ItemName = entity.GetString("ItemName");
                    return todo;
                }
            }
            return todo;
        }

        public void Upsert(string RowKey, Todo Todo)
        {
            var entity = new TableEntity(tableName, RowKey) {
                {"ItemName", Todo.ItemName },
                {"IsCompleted", Todo.IsCompleted }
            };
            tableClient.UpsertEntity(entity);
        }
    }
}
