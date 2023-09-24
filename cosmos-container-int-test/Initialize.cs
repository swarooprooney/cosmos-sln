namespace cosmos_container_int_test;

[TestClass]
public class Initialize
{

    [AssemblyInitialize]
    public static async Task InitializeCosmos(TestContext testContext)
    {
        await DockerCosmosDatabase.StartContainerAsync();
    }

    [AssemblyCleanup]
    public static async Task CleanUp()
    {
        await DockerCosmosDatabase.StopAsync();
    }
}