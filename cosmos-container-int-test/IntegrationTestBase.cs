using Microsoft.AspNetCore.Mvc.Testing;

namespace cosmos_container_int_test
{
    public class IntegrationTestBase
    {
        protected HttpClient _httpClient;

        public IntegrationTestBase()
        {
            var webApplicationFactory = new WebApplicationFactory<Program>();
            _httpClient = webApplicationFactory.CreateDefaultClient();
        }
    }
}