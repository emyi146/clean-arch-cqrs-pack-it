using PackIT.Shared.Abstractions.Exceptions;

namespace PackIT.Application.Exceptions;
internal class PackingListAlreadyExistsExceptions : PackItException
{
    public PackingListAlreadyExistsExceptions(string name)
        : base($"Packing list with name '{name}' already exists.")
    {
    }
}
