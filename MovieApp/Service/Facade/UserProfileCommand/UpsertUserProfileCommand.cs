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
    public class UpsertUserProfileCommand : UserProfile, IRequest
    {
        public string UserId { get; set; }
    }

    public class UpsertUserProfileCommandHandler : IRequestHandler<UpsertUserProfileCommand>
    {
        private readonly ITableStorage tableStorage;

        public UpsertUserProfileCommandHandler(ITableStorage tableStorage)
        {
            this.tableStorage = tableStorage;
        }

        public Task<Unit> Handle(UpsertUserProfileCommand request, CancellationToken cancellationToken)
        {
            tableStorage.UpsertUserProfile(request.UserId, request);

            return Task.FromResult(Unit.Value);
        }
    }
}
