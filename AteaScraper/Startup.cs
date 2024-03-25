using Microsoft.Extensions.DependencyInjection;
using System;
using AteaTask1.Core.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Refit;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using AteaTask1.Service.Services;
using AteaTask1.Api.Configuration;

[assembly: FunctionsStartup(typeof(AteaTask1.Api.StartUp))]

namespace AteaTask1.Api
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IStorageConfiguration, StorageConfiguration>();

            var sProvider = builder.Services.BuildServiceProvider();
            var storageConfiguration = sProvider.GetRequiredService<IStorageConfiguration>();
            var connectionString = storageConfiguration.ConnectionString;

            builder.Services.AddSingleton<TableClient>(serviceProvider => new TableClient(connectionString, storageConfiguration.TableName));
            builder.Services.AddSingleton<BlobContainerClient>(serviceProvider => new BlobContainerClient(connectionString, storageConfiguration.BlobContainerName));

            builder.Services.AddSingleton<ITableStorageService, TableStorageService>();
            builder.Services.AddSingleton<IBlobStorageService, BlobStorageService>();

            builder.Services.AddRefitClient<IPublicApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(storageConfiguration.ApiEndpoint));
        }
    }
}
