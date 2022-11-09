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
    public class GetAllTodoCommand : IRequest<List<Todo>>
    {
    }

    public class GetAllTodoCommandHandler : IRequestHandler<GetAllTodoCommand, List<Todo>>
    {
        private readonly ITodoTableClient  todoTableClient;

        public GetAllTodoCommandHandler(ITodoTableClient todoTableClient)
        {
            this.todoTableClient = todoTableClient;
        }

        public Task<List<Todo>> Handle(GetAllTodoCommand request, CancellationToken cancellationToken)
        {
            var todos = todoTableClient.GetAll();
            return Task.FromResult(todos);
        }
    }
}
