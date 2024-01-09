using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using http_pratice.Domain;
using http_pratice.DAL;
using Microsoft.Azure.Cosmos;
using System;

[assembly: FunctionsStartup(typeof(http_pratice.Startup))]

namespace http_pratice
{
    public class Startup : FunctionsStartup
    {
        private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("localsetting.json", true)
            .AddEnvironmentVariables()
            .Build();

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton(s =>
            {
                var connectionString = Configuration["CosmosDbConnectionString"];
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Please specify a valid CosmosDBConnection in the appSettings.json file or your Azure Functions Settings.");
                }

                return new CosmosClientBuilder(connectionString)
                    .WithSerializerOptions(new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase, IgnoreNullValues = true })
                    .Build();
            });
            builder.Services.AddStudentServicesDal();


            builder.Services.AddStudentServices();

        }
    }
}