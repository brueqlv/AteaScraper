using System;
using System.Threading.Tasks;
using AteaScraper.Interfaces;
using Azure;
using Azure.Data.Tables;
using Microsoft.WindowsAzure.Storage.Table;
using TableEntity = Azure.Data.Tables.TableEntity;

namespace AteaScraper.Services
{
    internal class TableStorageService : ITableStorageService
    {
        private readonly TableServiceClient _tableServiceClient;

        public TableStorageService(TableServiceClient tableServiceClient)
        {
            _tableServiceClient = tableServiceClient;
        }

        public async Task AddRequestAsync(string key, bool isSuccess)
        {
            var table = _tableServiceClient.GetTableClient("atea");
            await table.CreateIfNotExistsAsync();

            var tableEntity = new TableEntity("Request", Guid.NewGuid().ToString())
            {
                { "Request", key },
                { "Status", isSuccess }
            };

            await table.AddEntityAsync(tableEntity);
        }

        public async Task<Pageable<TableEntity>> GetLogsFromToAsync(DateTime from, DateTime to)
        {
            var table = _tableServiceClient.GetTableClient("atea");
            await table.CreateIfNotExistsAsync();

            var fromCondition =
                TableQuery.GenerateFilterConditionForDate("Timestamp", QueryComparisons.GreaterThanOrEqual, from);
            var toCondition =
                TableQuery.GenerateFilterConditionForDate("Timestamp", QueryComparisons.LessThanOrEqual, to);

            var filter = TableQuery.CombineFilters(fromCondition, TableOperators.And, toCondition);

            var result = table.Query<TableEntity>(filter);

            return result;
        }
    }
}