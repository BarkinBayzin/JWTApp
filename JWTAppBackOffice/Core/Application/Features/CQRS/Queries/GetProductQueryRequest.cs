using JWTAppBackOffice.Core.Application.DTOs;
using MediatR;

namespace JWTAppBackOffice.Core.Application.Features.CQRS.Queries
{
    public class GetProductQueryRequest: IRequest<ProductListDto>
    {
        public int Id { get; set; }

        public GetProductQueryRequest(int id)
        {
            Id = id;
        }
    }
}
