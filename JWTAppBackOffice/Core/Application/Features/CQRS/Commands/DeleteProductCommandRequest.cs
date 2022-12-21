using MediatR;

namespace JWTAppBackOffice.Core.Application.Features.CQRS.Commands
{
    public class DeleteProductCommandRequest : IRequest //geriye birşey dönmeyecek
    {
        public int Id{ get; set; }
        public DeleteProductCommandRequest(int id)
        {
            Id = id;
        }
    }
}
