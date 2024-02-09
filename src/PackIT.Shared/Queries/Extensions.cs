using Microsoft.Extensions.DependencyInjection;
using PackIT.Shared.Abstractions.Queries;
using System.Reflection;

namespace PackIT.Shared.Queries;
public static class Extensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();

        services.AddSingleton<IQueryDispatcher, InMemoryQueryDispatcher>();

        // Add Queries, using Scrutor, a NuGet package,
        // to scan the assembly for classes that implement
        // IQueryHandler<,> and register them as services.
        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
