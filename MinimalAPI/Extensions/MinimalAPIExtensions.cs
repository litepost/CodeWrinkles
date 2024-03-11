using Application;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace MinimalAPI;

public static class MinimalAPIExtensions
{
    public static void RegisterServices(this WebApplicationBuilder builder) 
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<SocialDbContext>(options => options.UseSqlite("Data source=MinimalAPICourse.db"));
        builder.Services.AddScoped<IPostRepository, PostRepository>();
    }

    public static void RegisterEndpointsDefinitions(this WebApplication app) 
    {
        var endpointDefintions = typeof(Program).Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition)) && !t.IsAbstract && !t.IsInterface)
            .Select(Activator.CreateInstance)
            .Cast<IEndpointDefinition>();

        foreach (var endpointDef in endpointDefintions) {
            endpointDef.RegisterEndpoints(app);
        }
    }
}
