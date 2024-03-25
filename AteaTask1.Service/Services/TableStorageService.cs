using AteaTask1.Core.Interfaces;
using AteaTask1.Core.Models;
using Azure;
using Azure.Data.Tables;
using Microsoft.WindowsAzure.Storage.Table;

namespace AteaTask1.Service.Services
{
    public class TableStorageService : ITableStorageService
    {
        private readonly TableClient _tableClient;

        public TableStorageService(TableClient tableClient)
        {
            _tableClient = tableClient;
        }

        public async Task AddRecordAsync(string key, bool isSuccess)
        {
            await _tableClient.CreateIfNotExistsAsync();

            var record = new Record()
            {
                RowKey = key,
                PartitionKey = Guid.NewGuid().ToString(),
                WasSuccessful = isSuccess
            };

            await _tableClient.AddEntityAsync(record);
        }

        public Pageable<Record> GetLogsFromTo(DateTime from, DateTime to)
        {
            var fromCondition =
                TableQuery.GenerateFilterConditionForDate("Timestamp", QueryComparisons.GreaterThanOrEqual, from);
            var toCondition =
                TableQuery.GenerateFilterConditionForDate("Timestamp", QueryComparisons.LessThanOrEqual, to);

            var filter = TableQuery.CombineFilters(fromCondition, TableOperators.And, toCondition);

            var result = _tableClient.Query<Record>(filter);

            return result;
        }
    }
}