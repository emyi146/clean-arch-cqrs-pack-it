using PackIT.Application.DTO;
using PackIT.Infrastructure.EF.Models;

namespace PackIT.Infrastructure.EF.Queries;
internal static class Extensions
{
    public static PackingListDto AsDto(this PackingListReadModel readModel)
        => new()
        {
            Id = readModel.Id,
            Name = readModel.Name,
            Localization = readModel.Localization.AsDto(),
            Items = readModel.Items?.Select(pi => pi.AsDto())?.ToList(),
        };

    public static LocalizationDto AsDto(this LocalizationReadModel readModel)
        => new()
        {
            City = readModel.City,
            Country = readModel.Country,
        };

    public static PackingItemDto AsDto(this PackingItemReadModel readModel)
        => new()
        {
            Name = readModel.Name,
            Quantity = readModel.Quantity,
            IsPacked = readModel.IsPacked,
        };
}
