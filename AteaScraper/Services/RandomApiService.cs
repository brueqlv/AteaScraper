//using System.IO;
//using System.Threading.Tasks;
//using Refit;

//namespace AteaScraper.Services
//{
//    public class RandomApiService
//    {
//        private readonly IPublicApi _publicApi;

//        public RandomApiService()
//        {
//            _publicApi = RestService.For<IPublicApi>("https://api.publicapis.org");
//        }

//        public async Task<ApiResponse<Stream>> GetRandomData()
//        {
//            return await _publicApi.GetRandomData();
//        }
//    }
//}
