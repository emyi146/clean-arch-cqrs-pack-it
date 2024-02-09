using PackIT.Application.Exceptions;
using PackIT.Domain.Repositories;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands;
internal sealed class RemovePackingItemHandler(IPackingListRepository repository) : ICommandHandler<RemovePackingItem>
{
    private readonly IPackingListRepository _repository = repository;

    public async Task HandleAsync(RemovePackingItem command)
    {
        var packingList = await _repository.GetAsync(command.PackingListId) ?? throw new PackingListNotFoundException(command.PackingListId);

        packingList.RemoveItem(command.Name);

        await _repository.UpdateAsync(packingList);
    }
}

