using PackIT.Application.DTO.External;
using PackIT.Application.Services;
using PackIT.Domain.ValueObjects;

namespace PackIT.Infrastructure.Services;
internal sealed class DumbWeatherService : IWeatherService
{

    public Task<WeatherDto> GetWeatherAsync(Localization localization)
        => Task.FromResult(new WeatherDto(Random.Shared.Next(5, 30)));
}
