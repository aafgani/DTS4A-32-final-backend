using MediatR;
using MovieApp.DAL.Todo;
using MovieApp.Service.Storage.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Facade.TodoCommand
{
    public class DeleteTodoCommand : Todo, IRequest
    {
    }

    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand>
    {
        private readonly ITodoTableClient todoTableClient;

        public DeleteTodoCommandHandler(ITodoTableClient todoTableClient)
        {
            this.todoTableClient = todoTableClient;
        }

        public Task<Unit> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            todoTableClient.DeleteByKey(request.Id);
            return Task.FromResult(Unit.Value);
        }
    }
}
