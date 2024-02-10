using PackIT.Application.DTO;
using PackIT.Application.Queries;
using PackIT.Domain.Repositories;
using PackIT.Shared.Abstractions.Queries;

namespace PackIT.Infrastructure.Queries.Handlers;
internal class SearchPackingListsHandler(IPackingListRepository packingListRepository) :
    IQueryHandler<SearchPackingLists, IEnumerable<PackingListDto>>
{
    private readonly IPackingListRepository _packingListRepository = packingListRepository;

    public Task<IEnumerable<PackingListDto>> HandleAsync(SearchPackingLists query)
    {
        throw new NotImplementedException();
    }
}
