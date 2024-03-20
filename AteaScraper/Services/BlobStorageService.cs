using AteaScraper.Interfaces;
using Azure.Storage.Blobs;
using System.IO;
using System.Threading.Tasks;

namespace AteaScraper.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobContainerClient _blobContainerClient;

        public BlobStorageService(BlobContainerClient blobContainerClient)
        {
            _blobContainerClient = blobContainerClient;
        }

        public async Task UploadJsonAsync(string key, Stream content)
        {
            await _blobContainerClient.CreateIfNotExistsAsync();

            var blob = _blobContainerClient.GetBlobClient($"{key}.json");
            await blob.UploadAsync(content);
        }

        public async Task<string> GetBlob(string blobName)
        {
            var blob = _blobContainerClient.GetBlobClient($"{blobName}.json");

            if (!await blob.ExistsAsync())
            {
                return null;
            }

            var blobResponse = await blob.DownloadContentAsync();
            return blobResponse.Value.Content.ToString();
        }
    }
}
