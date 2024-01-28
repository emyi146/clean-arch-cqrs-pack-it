using PackIT.Domain.Consts;
using PackIT.Domain.Entities;
using PackIT.Domain.Policies;
using PackIT.Domain.ValueObjects;

namespace PackIT.Domain.Factories;

internal class PackingListFactory : IPackingListFactory
{
    private readonly IEnumerable<IPackingItemsPolicy> _policies;

    public PackingListFactory(IEnumerable<IPackingItemsPolicy> policies) 
        => _policies = policies;

    public PackingList Create(PackingListId id, PackingListName name, Localization localization)
    => new(id, name, localization);

    public PackingList CreateWithDefaultItems(PackingListId id, PackingListName name, TravelDays days, Gender gender,
        Temperature temperature, Localization localization)
    {
        var data = new PolicyData(days,  gender, temperature, localization);
        var applicablePolicies = _policies.Where(p => p.IsApplicable(data));
        
        var packingList = Create(id, name, localization);

        var items = applicablePolicies.SelectMany(p => p.GenerateItems(data));
        packingList.AddItems(items);

        return packingList;
    }
}
