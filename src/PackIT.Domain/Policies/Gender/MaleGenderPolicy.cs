using PackIT.Domain.ValueObjects;

namespace PackIT.Domain.Policies.Gender;

internal class MaleGenderPolicy : IPackingItemsPolicy
{
    public bool IsApplicable(PolicyData data) 
        => data.Gender is Consts.Gender.Male;

    public IEnumerable<PackingItem> GenerateItems(PolicyData data)
    => new List<PackingItem>{
        new("Shave soap", 1),
        new("Male Underwear", data.Days),
        new("Book", (ushort)Math.Ceiling(data.Days / 7m)),
    };
}
