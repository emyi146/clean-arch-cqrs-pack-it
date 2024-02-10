using Microsoft.EntityFrameworkCore;
using PackIT.Application.DTO;
using PackIT.Application.Queries;
using PackIT.Infrastructure.EF.Contexts;
using PackIT.Infrastructure.EF.Models;
using PackIT.Shared.Abstractions.Queries;

namespace PackIT.Infrastructure.EF.Queries.Handlers;
internal class SearchPackingListsHandler :
    IQueryHandler<SearchPackingLists, IEnumerable<PackingListDto>>
{
    private readonly DbSet<PackingListReadModel> _packingLists;

    public SearchPackingListsHandler(ReadDbContext dbContext) => _packingLists = dbContext.PackingLists;

    public async Task<IEnumerable<PackingListDto>> HandleAsync(SearchPackingLists query)
    {
        var dbQuery = _packingLists
            .Include(pl => pl.Items)
            .AsQueryable();

        if (query.SearchPhrase is not null)
        {
            dbQuery = dbQuery.Where(pl =>
                Microsoft.EntityFrameworkCore.EF.Functions.ILike(pl.Name, $"%{query.SearchPhrase}%"));
        }

        return await dbQuery
            .Select(pl => pl.AsDto())
            .AsNoTracking() // Read-only operation can use AsNoTracking() to improve performance
            .ToListAsync();
    }

}
