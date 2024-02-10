using Microsoft.EntityFrameworkCore;
using PackIT.Domain.Entities;
using PackIT.Domain.ValueObjects;
using PackIT.Infrastructure.Config;
using PackIT.Infrastructure.EF.Models;

namespace PackIT.Infrastructure.EF.Contexts;
internal sealed class WriteDbContext : DbContext
{
    public DbSet<PackingList> PackingLists { get; set; }

    public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("packing");
        base.OnModelCreating(modelBuilder);

        var configuration = new WriteConfiguration();
        modelBuilder.ApplyConfiguration<PackingList>(configuration);
        modelBuilder.ApplyConfiguration<PackingItem>(configuration);
    }
}
