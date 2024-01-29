using Microsoft.Extensions.DependencyInjection;
using PackIT.Domain.Factories;
using PackIT.Domain.Policies;
using PackIT.Shared;

namespace PackIT.Application;
public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCommands();
        services.AddSingleton<IPackingListFactory, PackingListFactory>();


        // Add Policies, using Scrutor, a NuGet package,
        // to scan the assembly for classes that implement
        // IPackingItemsPolicy<> and register them as services.
        services.Scan(s => s.FromAssemblies(typeof(IPackingItemsPolicy).Assembly)
            .AddClasses(c => c.AssignableTo<IPackingItemsPolicy>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
