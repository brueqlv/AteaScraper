using System.IO;
using Refit;
using System.Threading.Tasks;

namespace AteaScraper.Services
{
    public interface IPublicApi
    {
        [Get("/random?auth=null")]
        Task<IApiResponse<Stream>> GetRandomData();
    }
}
