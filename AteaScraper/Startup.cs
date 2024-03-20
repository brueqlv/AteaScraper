using Microsoft.Extensions.DependencyInjection;
using System;
using AteaScraper.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Refit;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using AteaScraper.Interfaces;

[assembly: FunctionsStartup(typeof(AteaScraper.StartUp))]

namespace AteaScraper
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var connectionString = "UseDevelopmentStorage=true";
            var containerName = "atea";

            builder.Services.AddSingleton<TableServiceClient>(serviceProvider => new TableServiceClient(connectionString));
            builder.Services.AddSingleton<BlobContainerClient>(serviceProvider => new BlobContainerClient(connectionString, containerName));

            builder.Services.AddSingleton<ITableStorageService, TableStorageService>();
            builder.Services.AddSingleton<IBlobStorageService, BlobStorageService>();

            builder.Services.AddRefitClient<IPublicApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.publicapis.org"));
        }
    }
}
