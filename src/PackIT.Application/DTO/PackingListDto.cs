namespace PackIT.Application.DTO;

public class PackingListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public LocalizationDto Localization { get; set; }
    public List<PackingItemDto> Items { get; set; }
}
