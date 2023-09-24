using cosmos_container.Data;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.CosmosRepository.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCosmos();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }


public static class CosmosExtensions
{
    public static IServiceCollection AddCosmos(this IServiceCollection services,
        Action<CosmosClientOptions>? additionSetupAction = default)
    {
        services.AddCosmosRepository(options =>
        {
            options.ContainerPerItemType = true;
            options.ContainerBuilder.Configure<Person>(containerOptions => containerOptions
            .WithContainer("persons"));
        }, additionSetupAction);
        return services;
    }
}