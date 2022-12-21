using JWTAppBackOffice.Core.Application.DTOs;
using JWTAppBackOffice.Core.Application.Features.CQRS.Commands;
using JWTAppBackOffice.Core.Application.Interfaces;
using JWTAppBackOffice.Core.Domain;
using MediatR;

namespace JWTAppBackOffice.Core.Application.Features.CQRS.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest>
    {
        private readonly IRepository<Category> _repository;

        public UpdateCategoryCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var updatedCategory = await this._repository.GetByIdAsync(request.Id);
            if (updatedCategory != null)
            {
                updatedCategory.Definition = request.Definition;
                await this._repository.UpdateAsync(updatedCategory);
            }

            return Unit.Value;
        }
    }
}
