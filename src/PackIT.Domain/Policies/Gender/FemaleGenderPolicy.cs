using PackIT.Domain.ValueObjects;

namespace PackIT.Domain.Policies.Gender;

internal class FemaleGenderPolicy : IPackingItemsPolicy
{
    public bool IsApplicable(PolicyData data) 
        => data.Gender is Consts.Gender.Male;

    public IEnumerable<PackingItem> GenerateItems(PolicyData data)
    => new List<PackingItem>{
        new("Female underwear", data.Days),
        new("Menstrual pads", data.Days * 3u),
    };
}
