using Azure;
using System;
using System.Threading.Tasks;
using Azure.Data.Tables;

namespace AteaScraper.Interfaces
{
    public interface ITableStorageService
    {
        Task AddRequestAsync(string key, bool isSuccess);
        Task<Pageable<TableEntity>> GetLogsFromToAsync(DateTime from, DateTime to);
    }
}
