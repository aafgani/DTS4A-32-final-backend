using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Options;
using MovieApp.DAL.UserProfile;
using MovieApp.Service.Storage.Interface;
using MovieApp.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Storage
{
    public class TableStorage : ITableStorage
    {
        private readonly string _connectionString, _tableName;
        private TableClient tableClient;
        public TableStorage(IOptions<AppSettings> options)
        {
            var config = options.Value;
            _connectionString = config.TableStorage;
            _tableName = config.TableName;

            tableClient = new TableClient(_connectionString, _tableName);
            tableClient.CreateIfNotExists();
        }

        public void UpsertUserProfile(string rowKey, UserProfile userProfile )
        {
            var entity = new TableEntity(_tableName, rowKey) {
                {"FullName", userProfile.FullName }
            };           
            tableClient.UpsertEntity(entity);
        }

        public UserProfile GetUserProfileByKey(string rowKey)
        {
            var userProfile = new UserProfile();
            string filter = $"PartitionKey eq '{_tableName}' or RowKey eq '{rowKey}' ";
            Pageable<TableEntity> entities = tableClient.Query<TableEntity>(filter: filter);

            foreach (TableEntity entity in entities)
            {
                var key = entity.GetString("RowKey");
                if (key.Equals(rowKey))
                {
                    userProfile.FullName = entity.GetString("FullName");
                    return userProfile;
                }
            }

            return userProfile;
        }

        public List<UserProfile> GetUserProfile()
        {
            var userProfiles = new List<UserProfile>();
            string filter = $"PartitionKey eq '{_tableName}' ";
            Pageable<TableEntity> entities = tableClient.Query<TableEntity>(filter: filter);

            foreach (TableEntity entity in entities)
            {
                var key = entity.GetString("RowKey");
                if (!string.IsNullOrEmpty(key))
                {
                    userProfiles.Add(new UserProfile
                    {
                        UserId = key,
                        FullName = entity.GetString("FullName")
                    });
                }
            }

            return userProfiles;
        }
    }
}
