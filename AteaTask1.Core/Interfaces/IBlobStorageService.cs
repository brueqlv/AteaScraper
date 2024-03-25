namespace AteaTask1.Core.Interfaces
{
    public interface IBlobStorageService
    {
        Task UploadJsonAsync(string key, Stream content);
        Task<string?> GetJsonContentAsync(string blobName);
    }
}
