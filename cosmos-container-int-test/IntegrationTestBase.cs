using System.Net.Security;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.CosmosRepository.Options;
using Microsoft.Extensions.Localization;

namespace cosmos_container_int_test
{
    public class IntegrationTestBase
    {
        protected HttpClient _httpClient;

        public IntegrationTestBase()
        {
            var webApplicationFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    Action<CosmosClientOptions>? options = CreateClientOptions;
                    services.AddCosmos(localOptions =>
                    {
                        localOptions.HttpClientFactory = () =>
                        {
                            HttpMessageHandler httpMessageHandler = new HttpClientHandler()
                            {
                                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                            };
                            return new HttpClient(httpMessageHandler);
                        };
                        localOptions.ConnectionMode = ConnectionMode.Gateway;
                    });
                });

                builder.ConfigureAppConfiguration(config =>
                {
                    var cosmosConfig = new Dictionary<string, string?>
                    {
                        {
                            $"{nameof(RepositoryOptions)}:{nameof(RepositoryOptions.CosmosConnectionString)}",DockerCosmosDatabase.ConnectionString
                        },
                        {
                            $"{nameof(RepositoryOptions)}:{nameof(RepositoryOptions.DatabaseId)}","test"
                        }
                    };
                    config.AddInMemoryCollection(cosmosConfig.Select(x => new KeyValuePair<string, string?>(x.Key, x.Value)));
                });
            });
            _httpClient = webApplicationFactory.CreateClient();
        }

        private void CreateClientOptions(CosmosClientOptions options)
        {
            options.HttpClientFactory = () =>
            {
                HttpMessageHandler messageHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                return new HttpClient(messageHandler);
            };
            options.ConnectionMode = ConnectionMode.Gateway;
        }
    }
}