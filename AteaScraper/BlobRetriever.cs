using System.Threading.Tasks;
using AteaTask1.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AteaTask1.Api
{
    public class BlobRetriever
    {
        private readonly IBlobStorageService _blobStorageService;

        public BlobRetriever(IBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        [FunctionName("GetBlob")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var blobName = req.Query["blob"];

            log.LogInformation($"Received {req.Method} request for blob: {blobName}");

            var result = await _blobStorageService.GetBlob(blobName);

            if (result == null)
            {
                log.LogInformation($"Blob '{blobName}' not found.");
                return new NotFoundResult();
            }

            log.LogInformation($"Blob '{blobName}' retrieved successfully.");
            return new OkObjectResult(result);
        }
    }
}
