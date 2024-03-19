using System.IO;
using Refit;
using System.Threading.Tasks;

namespace AteaScraper
{
    public interface IPublicApi
    {
        [Get("/random?auth=null")]
        Task<ApiResponse<Stream>> GetRandomData();
    }
}
