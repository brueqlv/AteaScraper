using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Data.Tables;
using Microsoft.WindowsAzure.Storage.Table;
using TableEntity = Azure.Data.Tables.TableEntity;

namespace AteaScraper
{
    public static class Logs
    {
        [FunctionName("Logs")]
        public static async Task<IActionResult> Run(
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

            var serviceClient = new TableServiceClient("UseDevelopmentStorage=true");
            var table = serviceClient.GetTableClient("atea");
            await table.CreateIfNotExistsAsync();
            var fromCondition =
                TableQuery.GenerateFilterConditionForDate("Timestamp", QueryComparisons.GreaterThanOrEqual, from);
            var toCondition =
                TableQuery.GenerateFilterConditionForDate("Timestamp", QueryComparisons.LessThanOrEqual, to);

            var filter = TableQuery.CombineFilters(fromCondition, TableOperators.And, toCondition);

            var result = table.Query<TableEntity>(filter);


            log.LogInformation("This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response.");
                

            return new OkObjectResult(result);
        }
    }
}
