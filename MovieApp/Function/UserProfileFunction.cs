using System.Net;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using MovieApp.Service.Facade.UserProfileCommand;
using Newtonsoft.Json;

namespace MovieApp.Function
{
    public class UserProfileFunction
    {
        private readonly ILogger _logger;
        private readonly ISender _mediator;
        private const string functionName = nameof(UserProfileFunction);

        public UserProfileFunction(ILoggerFactory loggerFactory, ISender mediator)
        {
            _logger = loggerFactory.CreateLogger<UserProfileFunction>();
            _mediator = mediator;
        }

        [Function($"{functionName}-Upsert")]
        public async Task<HttpResponseData> UpsertAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var payload = await new StreamReader(req.Body).ReadToEndAsync();
            var command = JsonConvert.DeserializeObject<UpsertUserProfileCommand>(payload);
            await _mediator.Send(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("inserted successfully !");

            return response;
        }

        [Function($"{functionName}-Get")]
        public async Task<HttpResponseData> GetAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var payload = await new StreamReader(req.Body).ReadToEndAsync();
            var command = JsonConvert.DeserializeObject<GetUserProfileCommand>(payload);
            var data = await _mediator.Send(command);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json");

            response.WriteString(JsonConvert.SerializeObject(data));

            return response;
        }

    }
}
