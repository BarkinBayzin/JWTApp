using JWTAppBackOffice.Core.Application.DTOs;
using MediatR;

namespace JWTAppBackOffice.Core.Application.Features.CQRS.Queries
{
    public class GetCategoriesQueryRequest:IRequest<List<CategoryListDto>>
    {
    }
}
