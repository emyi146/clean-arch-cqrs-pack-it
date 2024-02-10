using Microsoft.EntityFrameworkCore;
using PackIT.Application.Services;
using PackIT.Infrastructure.EF.Models;

namespace PackIT.Infrastructure.EF.Services;
internal sealed class PostgresPackingListReadService : IPackingListReadService
{
    private readonly DbSet<PackingListReadModel> _packingList;

    public PostgresPackingListReadService(DbSet<PackingListReadModel> packingList)
        => _packingList = packingList;

    public Task<bool> ExistsByNameAsync(string name)
        => _packingList.AnyAsync(pl => pl.Name == name);
}
