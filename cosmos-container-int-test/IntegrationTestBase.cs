using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

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
                builder.ConfigureTestServices(services =>
                {
                    services.AddInMemoryCosmosRepository();
                });
            });
            _httpClient = webApplicationFactory.CreateDefaultClient();
        }
    }
}