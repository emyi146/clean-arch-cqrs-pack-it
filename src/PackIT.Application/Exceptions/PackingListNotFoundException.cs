using PackIT.Shared.Abstractions.Exceptions;

namespace PackIT.Application.Exceptions;
internal class PackingListNotFoundException : PackItException
{
    public PackingListNotFoundException(Guid packingListId)
        : base($"Packing list with ID '{packingListId}' not found.")
    {
    }
}
