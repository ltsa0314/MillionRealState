using MediatR;
using MillionRealState.Application.Features.Owner.Commands;
using MillionRealState.Domain.Aggregates.Owner;

namespace MillionRealState.Application.Features.Owner.CommandHandlers
{
    public class DeleteOwnerCommandHandler : IRequestHandler<DeleteOwnerCommand>
    {
        private readonly IOwnerWriteRepository _repo;

        public DeleteOwnerCommandHandler(IOwnerWriteRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(DeleteOwnerCommand request, CancellationToken cancellationToken)
        {
            await _repo.DeleteAsync(request.IdOwner);
        }
    }
}