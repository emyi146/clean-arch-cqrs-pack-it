using PackIT.Application.Exceptions;
using PackIT.Domain.Repositories;
using PackIT.Shared.Abstractions.Commands;

namespace PackIT.Application.Commands;
internal sealed class DeletePackingListHandler(IPackingListRepository repository) : ICommandHandler<DeletePackingList>
{
    private readonly IPackingListRepository _repository = repository;

    public async Task HandleAsync(DeletePackingList command)
    {
        var packingList = await _repository.GetAsync(command.Id) ?? throw new PackingListNotFoundException(command.Id);

        await _repository.DeleteAsync(packingList);
    }
}
