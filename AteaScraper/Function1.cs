using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AteaScraper
{
    public class Function1
    {
        [FunctionName("Function1")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            var client = new HttpClient();
            using HttpResponseMessage response = await client.GetAsync("https://api.publicapis.org/random?auth=null");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            log.LogInformation(responseBody);
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
