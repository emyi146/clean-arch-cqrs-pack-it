using PackIT.Domain.ValueObjects;

namespace PackIT.Domain.Policies.Universal;

internal class BasicPolicy : IPackingItemsPolicy
{
    private const uint MaximumQuantityOfClothes = 7;

    public bool IsApplicable(PolicyData data)
        => true;

    public IEnumerable<PackingItem> GenerateItems(PolicyData data)
    {
        var dailyFor1Week = Math.Min(data.Days, MaximumQuantityOfClothes);
        return new List<PackingItem>{
            new("Pants", dailyFor1Week),
            new("Socks", dailyFor1Week),
            new("T-Shirt", dailyFor1Week),
            new("Trousers", data.Days < 7 ? 1u : 2u),
            new("Shampoo", 1),
            new("Toothbrush", 1),
            new("Toothpaste", 1),
            new("Towel", 1),
            new("Bag pack", 1),
            new("Passport", 1),
            new("Phone Charger", 1),
        };
    }
}
