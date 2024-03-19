using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Refit;

namespace AteaScraper
{
    public class Function1
    {
        [FunctionName("Function1")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            var client = new HttpClient();
            using HttpResponseMessage response = await client.GetAsync("https://api.publicapis.org/random?auth=null");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();

            var publicApi = RestService.For<IPublicApi>("https://api.publicapis.org");
            var result = await publicApi.GetRandomData();

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
                    response.IsSuccessStatusCode
                }
            };

            await table.AddEntityAsync(tableEntity);

            var connection = "UseDevelopmentStorage=true";
            var containerName = "atea";

            var blobClient = new BlobContainerClient(connection, containerName);
            await blobClient.CreateIfNotExistsAsync();

            var blob = blobClient.GetBlobClient($"{key}.json");
            await blob.UploadAsync(responseStream);

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
