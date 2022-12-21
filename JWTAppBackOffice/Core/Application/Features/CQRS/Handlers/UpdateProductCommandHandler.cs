using JWTAppBackOffice.Core.Application.Features.CQRS.Commands;
using JWTAppBackOffice.Core.Application.Interfaces;
using JWTAppBackOffice.Core.Domain;
using MediatR;

namespace JWTAppBackOffice.Core.Application.Features.CQRS.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
    {
        private readonly IRepository<Product> _repository;

        public UpdateProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var updatedProduct = await this._repository.GetByIdAsync(request.Id);
            if (updatedProduct != null)
            {
                updatedProduct.Name = request.Name;
                updatedProduct.Price = request.Price;
                updatedProduct.Stock = request.Stock;
                updatedProduct.CategoryId= request.CategoryId;
                await this._repository.UpdateAsync(updatedProduct);
            }

            return Unit.Value;
        }
    }
}
