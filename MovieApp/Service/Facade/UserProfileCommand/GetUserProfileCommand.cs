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
    public class GetUserProfileCommand : IRequest<UserProfile>
    {
        public string UserId { get; set; }
    }

    public class GetUserProfileCommandHandler : IRequestHandler<GetUserProfileCommand, UserProfile>
    {
        private readonly ITableStorage tableStorage;

        public GetUserProfileCommandHandler(ITableStorage tableStorage)
        {
            this.tableStorage = tableStorage;
        }
        public Task<UserProfile> Handle(GetUserProfileCommand request, CancellationToken cancellationToken)
        {
            var userProfile = tableStorage.GetUserProfile(request.UserId);

            return Task.FromResult(userProfile);
        }
    }
}
