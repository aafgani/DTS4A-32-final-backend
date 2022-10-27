using MediatR;
using MovieApp.DAL.UserProfile;
using MovieApp.Service.Storage.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Facade.UserProfileCommand
{
    public class GetUserProfileCommand : IRequest<List<UserProfile>>
    {
        public string UserId { get; set; }
    }

    public class GetUserProfileCommandHandler : IRequestHandler<GetUserProfileCommand, List<UserProfile>>
    {
        private readonly ITableStorage tableStorage;

        public GetUserProfileCommandHandler(ITableStorage tableStorage)
        {
            this.tableStorage = tableStorage;
        }
        public Task<List<UserProfile>> Handle(GetUserProfileCommand request, CancellationToken cancellationToken)
        {
            var userProfiles = tableStorage.GetUserProfile();

            return Task.FromResult(userProfiles);
        }
    }
}
