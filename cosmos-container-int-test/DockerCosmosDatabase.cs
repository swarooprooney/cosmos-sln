using System.Net;
using System.Net.Sockets;
using DotNet.Testcontainers.Builders;
using Testcontainers.CosmosDb;

namespace cosmos_container_int_test;

public static class DockerCosmosDatabase
{
    public static bool IsRunning { get; private set; }

    private const string ContainerName = "persons";

    private static CosmosDbContainer? container;

    public static async Task StartContainerAsync()
    {
        if (!IsRunning)
        {
            container = new CosmosDbBuilder()
            .WithName(ContainerName)
            .WithExposedPort(8081)
            .WithExposedPort(10251)
            .WithExposedPort(10252)
            .WithExposedPort(10253)
            .WithExposedPort(10254)
            .WithPortBinding(8081)
            .WithPortBinding(10251)
            .WithPortBinding(10252)
            .WithPortBinding(10253)
            .WithPortBinding(10254)
            .WithEnvironment("AZURE_COSMOS_EMULATOR_PARTITION_COUNT", "1")
            .WithEnvironment("AZURE_COSMOS_EMULATOR_IP_ADDRESS_OVERRIDE", GetLocalIpAddress())
            .WithEnvironment("AZURE_COSMOS_EMULATOR_ENABLE_DATA_PERSISTENCE", "false")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged("Started"))
            .Build();
            await container.StartAsync();
            IsRunning = true;
        }

    }

    public static async Task StopAsync()
    {
        if (IsRunning && container is not null)
        {
            await container.StopAsync();
            await container.DisposeAsync();
        }

        IsRunning = false;
    }

    public static string ConnectionString => container?.GetConnectionString() ?? throw new ArgumentNullException("The cosmos container not started yet");

    private static string GetLocalIpAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());

        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }

        return string.Empty;
    }
}