using System;
using System.Threading.Tasks;
using AteaScraper.Services;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Refit;

namespace AteaScraper
{
    public class Function1
    {
        private readonly RandomApiService _randomApiService;

        public Function1( RandomApiService randomApiService)
        {
            _randomApiService = randomApiService;
        }

        [FunctionName("Function1")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            var responseStream = await _randomApiService.GetRandomData();

            var serviceClient = new TableServiceClient("UseDevelopmentStorage=true");
            var table = serviceClient.GetTableClient("atea");
            await table.CreateIfNotExistsAsync();
            var key = Guid.NewGuid();

            var tableEntity = new TableEntity("Request", Guid.NewGuid().ToString())
            {
                {
                    "Request",
                    key
                },
                {
                    "Status",
                    responseStream.IsSuccessStatusCode
                }
            };

            await table.AddEntityAsync(tableEntity);

            var connection = "UseDevelopmentStorage=true";
            var containerName = "atea";

            var blobClient = new BlobContainerClient(connection, containerName);
            await blobClient.CreateIfNotExistsAsync();

            var blob = blobClient.GetBlobClient($"{key}.json");
            await blob.UploadAsync(responseStream.Content);

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
