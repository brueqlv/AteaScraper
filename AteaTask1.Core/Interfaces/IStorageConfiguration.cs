namespace AteaTask1.Core.Interfaces
{
    public interface IStorageConfiguration
    {
        string ConnectionString { get; }
        string TableName { get; }
        string BlobContainerName { get; }
        string ApiEndpoint { get; }
    }
}
