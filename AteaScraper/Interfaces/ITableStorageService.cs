using Azure;
using System;
using System.Threading.Tasks;
using AteaScraper.Models;
using Azure.Data.Tables;

namespace AteaScraper.Interfaces
{
    public interface ITableStorageService
    {
        Task AddRecordAsync(string key, bool isSuccess);
        Task<Pageable<Record>> GetLogsFromToAsync(DateTime from, DateTime to);
    }
}
