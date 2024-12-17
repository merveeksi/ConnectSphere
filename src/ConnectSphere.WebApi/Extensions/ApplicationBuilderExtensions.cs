using ConnectSphere.Infrastructure.Persistence.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.WebApi.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        // Apply pending migrations
        if(context.Database.GetPendingMigrations().Any()) 
            context.Database.Migrate();

        return app;
    }
}