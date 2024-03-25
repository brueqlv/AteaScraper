using Refit;

namespace AteaTask1.Core.Interfaces
{
    public interface IPublicApi
    {
        [Get("/random")]
        Task<IApiResponse<Stream>> GetRandomData([Query("auth")] string? auth = null);
    }
}
