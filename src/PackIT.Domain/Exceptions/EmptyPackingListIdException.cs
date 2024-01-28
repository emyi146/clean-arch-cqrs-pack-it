using PackIT.Shared.Abstractions.Exceptions;

namespace PackIT.Domain.Exceptions;
internal class EmptyPackingListIdException : PackItException
{
    public EmptyPackingListIdException(): base("Packing list id cannot be empty.")
    {
    }
}
