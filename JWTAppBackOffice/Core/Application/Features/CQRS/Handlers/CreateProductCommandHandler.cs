using JWTAppBackOffice.Core.Application.Features.CQRS.Commands;
using JWTAppBackOffice.Core.Application.Interfaces;
using JWTAppBackOffice.Core.Domain;
using MediatR;

namespace JWTAppBackOffice.Core.Application.Features.CQRS.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest>
    {
        private readonly IRepository<Product> _repository;

        public CreateProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await this._repository.CreateAsync(new Product { 
                CategoryId = request.CategoryId,
                Name= request.Name,
                Price= request.Price,
                Stock = request.Stock,
            });

            return Unit.Value;
        }
    }
}
