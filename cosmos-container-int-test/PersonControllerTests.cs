using Shouldly;

namespace cosmos_container_int_test;

[TestClass]
public class PersonControllerTests : IntegrationTestBase
{
    [TestMethod]
    public async Task GetPersons_WhenPersonDoesntExist_ReturnsNotFound()
    {
        var id = Guid.NewGuid();
        var response = await _httpClient.GetAsync($"Person/{id}");
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
    }
}
