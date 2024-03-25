using Azure;
using AteaTask1.Core.Models;

namespace AteaTask1.Core.Interfaces
{
    public interface ITableStorageService
    {
        Task AddRecordAsync(string key, bool isSuccess);
        Pageable<Record> GetLogsAsync(DateTime from, DateTime to);
    }
}
