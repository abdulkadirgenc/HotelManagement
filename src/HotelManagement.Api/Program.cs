using Autofac.Extensions.DependencyInjection;
using HotelManagement.Infrastructure.Data;
using HotelManagement.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Configuration.AddEnvironmentVariables();

builder.ConfigureApplicationServices();

var app = builder.Build();

app.ConfigureRequestPipeline(app.Environment);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var hotelManagementContextSeed = services.GetRequiredService<HotelManagementContextSeed>();
        hotelManagementContextSeed.SeedAsync().Wait();
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.Run();
