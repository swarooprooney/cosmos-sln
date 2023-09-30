using cosmos_container.Inbound.Dtos;
using cosmos_container.OutBound.Dtos;
using Microsoft.Azure.Cosmos;
using Shouldly;

namespace cosmos_container_int_test;

[TestClass]
public class PersonControllerTests : IntegrationTestBase
{
    [TestMethod]
    public async Task GetPerson_WhenPersonDoesntExist_ReturnsNotFound()
    {
        var id = Guid.NewGuid();
        var response = await _httpClient.GetAsync($"Person/{id}");
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
    }

    [TestMethod]
    public async Task Create_WhenPersonSaved_ReturnsOk()
    {
        const string name = "Jane";
        const int Age = 3;
        var person = new CreatePersonDto
        {
            Name = name,
            Age = Age
        };
        var response = await _httpClient.PostAsJsonAsync($"Person", person);
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        var createdPerson = await response.Content.ReadFromJsonAsync<CreatedPersonDto>();
        createdPerson.ShouldNotBeNull();
        createdPerson.Age.ShouldBe(Age);
        createdPerson.Name.ShouldBe(name);
        Guid.TryParse(createdPerson.Id, out _).ShouldBe(true);
    }

    [TestMethod]
    public async Task GetPerson_WhenPersonExists_ReturnsPerson()
    {
        const string name = "Jane";
        const int Age = 3;
        var person = new CreatePersonDto
        {
            Name = name,
            Age = Age
        };
        var response = await _httpClient.PostAsJsonAsync($"Person", person);
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        var createdPerson = await response.Content.ReadFromJsonAsync<CreatedPersonDto>();
        var getPersonResponse = await _httpClient.GetAsync($"Person/{createdPerson?.Id}");
        getPersonResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        var getPerson = await getPersonResponse.Content.ReadFromJsonAsync<CreatedPersonDto>();
        getPerson.ShouldNotBeNull();
        getPerson.Age.ShouldBe(Age);
        getPerson.Name.ShouldBe(name);
        Guid.TryParse(getPerson.Id, out _).ShouldBe(true);
    }

    [TestMethod]
    public async Task CheckIfCosmosWorks()
    {
        CosmosClientOptions options = new()
        {
            HttpClientFactory = () => new HttpClient(new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            }),
            ConnectionMode = ConnectionMode.Gateway
        };

        using CosmosClient client = new CosmosClient(DockerCosmosDatabase.ConnectionString, options);

        Database database = await client.CreateDatabaseIfNotExistsAsync(id: "cosmicworks", throughput: 400);
        Container container = await database.CreateContainerIfNotExistsAsync(id: "products", partitionKeyPath: "/id");

        var item = new
        {
            id = "68719518371",
            name = "Kiama classic surfboard"
        };

        await container.UpsertItemAsync(item);
    }
}
