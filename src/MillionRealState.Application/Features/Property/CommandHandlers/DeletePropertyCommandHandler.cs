using MediatR;
using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Application.Features.Property.Commands;

namespace MillionRealState.Application.Features.Property.CommandHandlers
{
    public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand>
    {
        private readonly IPropertyWriteRepository _repo;

        public DeletePropertyCommandHandler(IPropertyWriteRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            await _repo.DeleteAsync(request.IdProperty);
        }
    }
}