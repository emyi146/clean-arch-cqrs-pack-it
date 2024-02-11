using PackIT.Application.Exceptions;
using PackIT.Domain.Repositories;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands;
internal sealed class RemovePackingListHandler(IPackingListRepository repository) : ICommandHandler<RemovePackingList>
{
    private readonly IPackingListRepository _repository = repository;

    public async Task HandleAsync(RemovePackingList command)
    {
        var packingList = await _repository.GetAsync(command.Id) ?? throw new PackingListNotFoundException(command.Id);

        await _repository.DeleteAsync(packingList);
    }
}
