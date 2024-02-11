using PackIT.Application.Commands;
using PackIT.Application.DTO.External;
using PackIT.Application.Exceptions;
using PackIT.Application.Services;
using PackIT.Domain.Consts;
using PackIT.Domain.Entities;
using PackIT.Domain.Factories;
using PackIT.Domain.Repositories;
using PackIT.Domain.ValueObjects;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.UnitTests.Application;
public class CreatePackingListWithItemsHandlerTest
{
    Task Act(CreatePackingListWithItems command)
        => _commandHandler.HandleAsync(command);

    [Fact]
    public async Task HandleAsync_Throws_PackingListAlreadyExistsException_When_List_With_Same_Name_Already_Exists()
    {
        // Arrange
        var command = new CreatePackingListWithItems(Guid.NewGuid(), "MyList", 5, Gender.Male, default);
        _readService.ExistsByNameAsync(command.Name).Returns(true);

        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<PackingListAlreadyExistsException>();
    }

    [Fact]
    public async Task HandleAsync_Throws_MissingLocalizationWeatherException_When_Wheather_Is_Not_Returned_From_Service()
    {
        // Arrange
        var command = new CreatePackingListWithItems(
            Guid.NewGuid(), "MyList", 5, Gender.Male, new LocalizationWriteModel("Madrid", "Spain"));
        _readService.ExistsByNameAsync(command.Name).Returns(false);
        _weatherService.GetWeatherAsync(Arg.Any<Localization>()).Returns(default(WeatherDto));

        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<MissingLocalizationWeatherException>();
    }

    [Fact]
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        // Arrange
        var command = new CreatePackingListWithItems(
            Guid.NewGuid(), "MyList", 5, Gender.Male, new LocalizationWriteModel("Madrid", "Spain"));
        _readService.ExistsByNameAsync(command.Name).Returns(false);
        _weatherService.GetWeatherAsync(Arg.Any<Localization>()).Returns(new WeatherDto(12));
        _factory.CreateWithDefaultItems(command.Id, command.Name, command.Days, command.Gender, Arg.Any<Temperature>(), Arg.Any<Localization>())
            .Returns(default(PackingList));

        // Act
        await Act(command);

        // Assert
        await _repository.Received(1).AddAsync(Arg.Any<PackingList>());
    }

    #region ARRANGE

    private readonly ICommandHandler<CreatePackingListWithItems> _commandHandler;
    private readonly IPackingListRepository _repository;
    private readonly IWeatherService _weatherService;
    private readonly IPackingListReadService _readService;
    private readonly IPackingListFactory _factory;

    public CreatePackingListWithItemsHandlerTest()
    {
        _repository = Substitute.For<IPackingListRepository>();
        _weatherService = Substitute.For<IWeatherService>();
        _readService = Substitute.For<IPackingListReadService>();
        _factory = Substitute.For<IPackingListFactory>();

        _commandHandler = new CreatePackingListWithItemsHandler(_repository, _factory, _readService, _weatherService);
    }

    #endregion
}
