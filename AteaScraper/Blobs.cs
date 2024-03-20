using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AteaScraper.Interfaces;

namespace AteaScraper
{
    public class Blobs
    {
        private readonly IBlobStorageService _blobStorageService;

        public Blobs(IBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        [FunctionName("Blobs")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var blobName = req.Query["blob"];
            var result = await _blobStorageService.GetBlob(blobName);

            if (result == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result);
        }
    }
}
