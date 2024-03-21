namespace AteaTask1.Core.Configuration
{
    public interface IStorageConfiguration
    {
        string ConnectionString { get; }
        string TableName { get; }
        string BlobContainerName { get; }
    }
}
