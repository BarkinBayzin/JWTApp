using JWTAppBackOffice.Core.Application.Features.CQRS.Commands;
using JWTAppBackOffice.Core.Application.Interfaces;
using JWTAppBackOffice.Core.Domain;
using MediatR;

namespace JWTAppBackOffice.Core.Application.Features.CQRS.Handlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest>
    {
        private readonly IRepository<Category> _repository;

        public DeleteCategoryCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var deletedEntity = await this._repository.GetByIdAsync(request.Id);
            if (deletedEntity != null)
            {
                await this._repository.RemoveAsync(deletedEntity);
            }
            return Unit.Value;
        }
    }
}
