using Microsoft.Extensions.DependencyInjection;
using System;
using AteaScraper.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AteaScraper.StartUp))]

namespace AteaScraper
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient<IPublicApi>()
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri("https://api.publicapis.org");
                });

            builder.Services.AddScoped<RandomApiService>();
        }
    }
}
