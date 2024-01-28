using PackIT.Domain.ValueObjects;
using PackIT.Shared.Abstractions.Exceptions;
using System;

namespace PackIT.Domain.Exceptions;
internal class InvalidTemperatureException : PackItException
{
    public double Temperature { get; }

    public InvalidTemperatureException(double temperature) : base($"Value '{temperature}' is invalid temperature.")
    {
        Temperature = temperature;
    }

}