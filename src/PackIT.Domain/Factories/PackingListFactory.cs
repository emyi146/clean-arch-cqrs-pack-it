using PackIT.Domain.Consts;
using PackIT.Domain.Entities;
using PackIT.Domain.ValueObjects;

namespace PackIT.Domain.Factories;

public class PackingListFactory : IPackingListFactory
{
    public PackingList Create(PackingListId id, PackingListName name, Localization localization)
    {
        return new PackingList(id, name, localization);
    }

    public PackingList CreateWithDefaultItems(PackingListId id, PackingListName name, TravelDays days, Gender gender,
        Temperature temperature, Localization localization)
    {
        // TODO
        return new PackingList(id, name, localization);
    }
}
