using Refit;
using System.Threading.Tasks;

namespace AteaScraper
{
    public interface IPublicApi
    {
        [Get("/random?auth=null")]
        Task<string> GetRandomData();
    }
}
