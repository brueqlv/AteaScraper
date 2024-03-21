namespace AteaTask1.Core.Configuration
{
    public class StorageConfiguration : IStorageConfiguration
    {
        public string ConnectionString { get; }
        public string TableName { get; }
        public string BlobContainerName { get; }

        public StorageConfiguration()
        {
            ConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            TableName = Environment.GetEnvironmentVariable("TableName");
            BlobContainerName = Environment.GetEnvironmentVariable("BlobContainerName");
        }
    }
}
