using System.Net;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using MovieApp.Service.Facade.TodoCommand;
using Newtonsoft.Json;

namespace MovieApp.Function
{
    public class TodoFunction
    {
        private readonly ILogger _logger;
        private readonly ISender _mediator;
        private const string functionName = nameof(TodoFunction);
        private const string _basePath = "todo";

        public TodoFunction(ILoggerFactory loggerFactory, ISender mediator)
        {
            _logger = loggerFactory.CreateLogger<TodoFunction>();
            _mediator = mediator;
        }

        [Function($"{functionName}-Upsert")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous,"post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var payload = await new StreamReader(req.Body).ReadToEndAsync();
            var command = JsonConvert.DeserializeObject<UpsertTodoCommand>(payload);
            await _mediator.Send(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("inserted/updated successfully !");

            return response;
        }

        [Function($"{functionName}-GetAll")]
        public async Task<HttpResponseData> GetAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req, string id)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var payload = await new StreamReader(req.Body).ReadToEndAsync();
            var command = new GetAllTodoCommand();       
            var data = await _mediator.Send(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json");

            response.WriteString(JsonConvert.SerializeObject(data));

            return response;
        }

        [Function($"{functionName}-Delete")]
        public async Task<HttpResponseData> DeleteAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var payload = await new StreamReader(req.Body).ReadToEndAsync();
            var command = JsonConvert.DeserializeObject<DeleteTodoCommand>(payload);
            await _mediator.Send(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("deleted successfully !");

            return response;
        }

        [Function($"{functionName}-Delete-By-Id")]
        public async Task<HttpResponseData> DeleteByIdAsync([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = _basePath + "/{id}")] HttpRequestData req, string id)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var command = new DeleteTodoCommand { Id = id };
            await _mediator.Send(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("deleted successfully !");

            return response;
        }
    }
}
