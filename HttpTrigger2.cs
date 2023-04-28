using System.Net;
using Company.Function.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class HttpTrigger2
    {
        private readonly ILogger _logger;

        public HttpTrigger2(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HttpTrigger2>();
        }

        [Function("HttpTrigger2")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req, [CosmosDBInput("%DBName%", "%collName%")] IEnumerable<Person> persons)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString(persons.Select(p => p.Firstname).Aggregate((a, b) => a + ", " + b));

            return response;
        }
    }
}
