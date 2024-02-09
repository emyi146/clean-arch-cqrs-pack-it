using PackIT.Application.Exceptions;
using PackIT.Domain.Repositories;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands;
internal sealed class PackItemHandler(IPackingListRepository repository) : ICommandHandler<PackItem>
{
    private readonly IPackingListRepository _repository = repository;

    public async Task HandleAsync(PackItem command)
    {
        var packingList = await _repository.GetAsync(command.PackingListId) ?? throw new PackingListNotFoundException(command.PackingListId);

        packingList.PackItem(command.Name);

        await _repository.UpdateAsync(packingList);
    }
}
