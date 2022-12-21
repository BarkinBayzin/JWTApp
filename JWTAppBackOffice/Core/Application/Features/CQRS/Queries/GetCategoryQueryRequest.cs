using JWTAppBackOffice.Core.Application.DTOs;
using MediatR;

namespace JWTAppBackOffice.Core.Application.Features.CQRS.Queries
{
    public class GetCategoryQueryRequest : IRequest<CategoryListDto>
    {
        public int Id { get; set; }
        public GetCategoryQueryRequest(int id)
        {
            Id = id;
        }
    }
}
