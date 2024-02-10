using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PackIT.Infrastructure.EF.Initializer;
internal sealed class AppInitializer : IHostedService
{
    IServiceProvider _serviceProvider;

    public AppInitializer(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(a => typeof(DbContext).IsAssignableFrom(a) && !a.IsInterface && a != typeof(DbContext));

        using var scope = _serviceProvider.CreateScope();
        foreach (var dbContextType in dbContextTypes)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService(dbContextType) as DbContext;
            if (dbContext == null)
            {
                continue;
            }
            await dbContext.Database.MigrateAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
