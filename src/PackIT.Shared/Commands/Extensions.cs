using Microsoft.Extensions.DependencyInjection;
using PackIT.Shared.Abstractions.Commands;
using System.Reflection;

namespace PackIT.Shared.Commands;
public static class Extensions
{
    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();

        services.AddSingleton<ICommandDispatcher, InMemoryCommandDispatcher>();

        // Add Commands, using Scrutor, a NuGet package,
        // to scan the assembly for classes that implement
        // ICommandHandler<TCommand> and register them as services.
        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
