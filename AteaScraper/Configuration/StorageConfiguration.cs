using System;
using AteaTask1.Core.Interfaces;

namespace AteaTask1.Api.Configuration
{
    public class StorageConfiguration : IStorageConfiguration
    {
        public string ConnectionString { get; }
        public string TableName { get; }
        public string BlobContainerName { get; }
        public string ApiEndpoint { get; }

        public StorageConfiguration()
        {
            ConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            TableName = Environment.GetEnvironmentVariable("TableName");
            BlobContainerName = Environment.GetEnvironmentVariable("BlobContainerName");
            ApiEndpoint = Environment.GetEnvironmentVariable("ApiEndpoint");
        }
    }
}
