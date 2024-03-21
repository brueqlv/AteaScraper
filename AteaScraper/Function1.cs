using System;
using System.Threading.Tasks;
using AteaScraper.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AteaScraper
{
    public class Function1
    {
        private readonly IPublicApi _publicApi;
        private readonly ITableStorageService _tableStorageService;
        private readonly IBlobStorageService _blobStorageService;

        public Function1(IPublicApi publicApi, ITableStorageService tableStorageService, IBlobStorageService blobStorageService)
        {
            _publicApi = publicApi;
            _tableStorageService = tableStorageService;
            _blobStorageService = blobStorageService;
        }

        [FunctionName("Function1")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            var responseStream = await _publicApi.GetRandomData();
            var key = Guid.NewGuid().ToString();

            await _tableStorageService.AddRecordAsync(key, responseStream.IsSuccessStatusCode);
            await _blobStorageService.UploadJsonAsync(key, responseStream.Content);

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
