using PackIT.Application.Exceptions;
using PackIT.Application.Services;
using PackIT.Domain.Factories;
using PackIT.Domain.Repositories;
using PackIT.Domain.ValueObjects;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands;
internal sealed class CreatePackingListWithItemsHandler(
    IPackingListRepository repository,
    IPackingListFactory factory,
    IPackingListReadService readService,
    IWeatherService weatherService
    ) : ICommandHandler<CreatePackingListWithItems>
{
    private readonly IPackingListRepository _repository = repository;
    private readonly IPackingListFactory _factory = factory;
    private readonly IPackingListReadService _readService = readService;
    private readonly IWeatherService _weatherService = weatherService;

    public async Task HandleAsync(CreatePackingListWithItems command)
    {
        var (id, name, days, gender, localizationWriteModel) = command;

        if (await _readService.ExistsByNameAsync(name))
        {
            throw new PackingListAlreadyExistsException(name);
        }

        var localization = new Localization(localizationWriteModel.City, localizationWriteModel.Country);
        var weather = await _weatherService.GetWeatherAsync(localization) ?? throw new MissingLocalizationWeatherException(localization);

        var packingList = _factory.CreateWithDefaultItems(
            id, name, days, gender, weather.Temperature, localization);

        await _repository.AddAsync(packingList);
    }
}
