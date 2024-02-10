using Microsoft.Extensions.DependencyInjection;

namespace PackIT.Application;
public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddQueries();
        return services;
    }
}
