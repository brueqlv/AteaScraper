using System;
using System.Threading.Tasks;
using AteaTask1.Core.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AteaTask1.Api
{
    public class StoreData
    {
        private readonly IPublicApi _publicApi;
        private readonly ITableStorageService _tableStorageService;
        private readonly IBlobStorageService _blobStorageService;

        public StoreData(IPublicApi publicApi, ITableStorageService tableStorageService, IBlobStorageService blobStorageService)
        {
            _publicApi = publicApi;
            _tableStorageService = tableStorageService;
            _blobStorageService = blobStorageService;
        }

        [FunctionName("StoreData")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function started execution at: {DateTime.Now}");
            log.LogInformation("Retrieving random data from the public API...");

            var responseStream = await _publicApi.GetRandomData();
            var key = Guid.NewGuid().ToString();

            log.LogInformation("Storing data in table storage...");
            await _tableStorageService.AddRecordAsync(key, responseStream.IsSuccessStatusCode);

            log.LogInformation("Storing data in blob storage...");
            await _blobStorageService.UploadJsonAsync(key, responseStream.Content);

            log.LogInformation($"C# Timer trigger function executed successfully at: {DateTime.Now}");
        }
    }
}
