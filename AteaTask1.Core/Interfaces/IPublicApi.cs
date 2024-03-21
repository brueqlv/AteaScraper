using Refit;

namespace AteaTask1.Core.Interfaces
{
    public interface IPublicApi
    {
        [Get("/random?auth=null")]
        Task<IApiResponse<Stream>> GetRandomData();
    }
}
