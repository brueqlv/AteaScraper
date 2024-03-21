using Azure;
using AteaTask1.Core.Models;

namespace AteaTask1.Core.Interfaces
{
    public interface ITableStorageService
    {
        Task AddRecordAsync(string key, bool isSuccess);
        Task<Pageable<Record>> GetLogsFromToAsync(DateTime from, DateTime to);
    }
}
