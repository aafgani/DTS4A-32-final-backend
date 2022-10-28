using MediatR;
using MovieApp.Service.Storage.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Facade.UserProfileCommand
{
    public class DeleteUserProfileCommand : IRequest
    {
        public string UserId { get; set; }
    }

    public class DeleteUserProfileCommandHandler : IRequestHandler<DeleteUserProfileCommand>
    {
        private readonly ITableStorage tableStorage;

        public DeleteUserProfileCommandHandler(ITableStorage tableStorage)
        {
            this.tableStorage = tableStorage;
        }

        public Task<Unit> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken)
        {
            tableStorage.DeleteUserProfileByKet(request.UserId);
            return Task.FromResult(Unit.Value);
        }
    }
}
