using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AteaScraper.Interfaces;

namespace AteaScraper
{
    public class Logs
    {
        private readonly ITableStorageService _tableStorageService;

        public Logs(ITableStorageService tableStorageService)
        {
            _tableStorageService = tableStorageService;
        }

        [FunctionName("Logs")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!DateTime.TryParse(req.Query["from"], out var from))
            {
                return new BadRequestResult();
            }

            if (!DateTime.TryParse(req.Query["to"], out var to))
            {
                return new BadRequestResult();
            }

            var result = _tableStorageService.GetLogsFromToAsync(from, to).Result;

            log.LogInformation("This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response.");
                
            return new OkObjectResult(result);
        }
    }
}
