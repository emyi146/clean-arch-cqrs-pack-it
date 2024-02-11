using PackIT.Application.Exceptions;
using PackIT.Domain.Repositories;
using PackIT.Domain.ValueObjects;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands;
public class AddPackingItemHandler(IPackingListRepository repository) : ICommandHandler<AddPackingItem>
{
    private readonly IPackingListRepository _repository = repository;

    public async Task HandleAsync(AddPackingItem command)
    {
        var packingList = await _repository.GetAsync(command.PackingListId) ?? throw new PackingListNotFoundException(command.PackingListId);

        var packingItem = new PackingItem(command.Name, command.Quantity);
        packingList.AddItem(packingItem);

        await _repository.UpdateAsync(packingList);
    }
}
