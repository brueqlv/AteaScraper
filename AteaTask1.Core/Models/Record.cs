using Azure;
using Azure.Data.Tables;

namespace AteaTask1.Core.Models
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
