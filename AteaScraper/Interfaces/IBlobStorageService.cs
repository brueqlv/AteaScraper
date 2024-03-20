using System.IO;
using System.Threading.Tasks;

namespace AteaScraper.Interfaces
{
    public interface IBlobStorageService
    {
        Task UploadJsonAsync(string key, Stream content);
        Task<string> GetBlob(string blobName);
    }
}
