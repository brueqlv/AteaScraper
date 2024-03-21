using Azure;
using Azure.Data.Tables;
using System;

namespace AteaScraper.Models
{
    public class Record : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public bool WasSuccessful { get; set; }
    }
}
