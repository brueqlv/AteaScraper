using System;
using System.Threading.Tasks;
using AteaTask1.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AteaTask1.Api
{
    public class LogRetriever
    {
        private readonly ITableStorageService _tableStorageService;

        public LogRetriever(ITableStorageService tableStorageService)
        {
            _tableStorageService = tableStorageService;
        }

        [FunctionName("GetLogsBetweenDates")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            if (!DateTime.TryParse(req.Query["from"], out var from) 
                || !DateTime.TryParse(req.Query["to"], out var to))
            {
                log.LogWarning("Invalid date format in request. Expected format: YYYY-MM-DDTHH:MM:SS");
                return new BadRequestResult();
            }

            log.LogInformation($"Received {req.Method} request for logs between {from} and {to}");

            var result = _tableStorageService.GetLogsFromTo(from, to);

            log.LogInformation($"Retrieved {result.AsPages().Count()} logs between {from} and {to}");

            return new OkObjectResult(result);
        }
    }
}
