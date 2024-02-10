using PackIT.Application.DTO;
using PackIT.Application.Queries;
using PackIT.Domain.Repositories;
using PackIT.Shared.Abstractions.Queries;

namespace PackIT.Infrastructure.Queries.Handlers;
public class GetPackingListHandler(IPackingListRepository packingListRepository)
    : IQueryHandler<GetPackingList, PackingListDto>
{
    private readonly IPackingListRepository _packingListRepository = packingListRepository;

    public async Task<PackingListDto> HandleAsync(GetPackingList query)
    {
        return null;
    }
}
