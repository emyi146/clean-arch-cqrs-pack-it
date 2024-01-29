using PackIT.Application.Exceptions;
using PackIT.Application.Services;
using PackIT.Domain.Factories;
using PackIT.Domain.Repositories;
using PackIT.Domain.ValueObjects;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands;
public class CreatePackingListWithItemsHandler(
    IPackingListRepository repository,
    IPackingListFactory factory,
    IPackingListReadService readService) : ICommandHandler<CreatePackingListWithItems>
{
    private readonly IPackingListRepository _repository = repository;
    private readonly IPackingListFactory _factory = factory;
    private readonly IPackingListReadService _readService = readService;

    public async Task HandleAsync(CreatePackingListWithItems command)
    {
        var (id, name, days, gender, localization) = command;

        if (await _readService.ExistsByNameAsync(name))
        {
            throw new PackingListAlreadyExistsExceptions(name);
        }

        var (city, country) = localization;
        var packingList = _factory.CreateWithDefaultItems(
            // TODO: Add weather service to get temperature.
            id, name, days, gender, 10d, Localization.Create($"{city},{country}"));

        await _repository.AddAsync(packingList);
    }
}
