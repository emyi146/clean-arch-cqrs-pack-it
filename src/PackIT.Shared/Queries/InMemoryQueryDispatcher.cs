using Microsoft.Extensions.DependencyInjection;
using PackIT.Shared.Abstractions.Queries;

namespace PackIT.Shared.Queries;
internal sealed class InMemoryQueryDispatcher(IServiceProvider serviceProvider) : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
    {
        using var scope = _serviceProvider.CreateScope();
        // Get the a template of the handler type for the query type and result type and make it a concrete type with the query type and result type as generic arguments
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        // Get the handler from the service provider
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);
        // Get the method name of the handle method
        var handleAsyncMethodName = nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync);

        // Invoke the handle method of the handler with the query as argument, and return the result as a Task<TResult>
        return await (Task<TResult>)handlerType.GetMethod(handleAsyncMethodName)?
            .Invoke(handler, new[] { query })!;
    }
}
