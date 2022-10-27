using MovieApp.DAL.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Storage.Interface
{
    public interface ITableStorage
    {
        void UpsertUserProfile(string rowKey, UserProfile userProfile);

        UserProfile GetUserProfile(string rowKey);

    }
}
