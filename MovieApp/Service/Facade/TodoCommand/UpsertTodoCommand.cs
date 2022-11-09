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
    public class UpsertTodoCommand : Todo, IRequest
    {
    }

    public class UpsertTodoCommandHandler : IRequestHandler<UpsertTodoCommand>
    {
        private readonly ITodoTableClient todoTableClient;

        public UpsertTodoCommandHandler(ITodoTableClient todoTableClient)
        {
            this.todoTableClient = todoTableClient;
        }

        public Task<Unit> Handle(UpsertTodoCommand request, CancellationToken cancellationToken)
        {
            var rowKey = string.IsNullOrEmpty(request.Id)? Guid.NewGuid().ToString() : request.Id;
            todoTableClient.Upsert(rowKey, request);

            return Task.FromResult(Unit.Value);
        }
    }
}
