using AutoMapper;
using JWTAppBackOffice.Core.Application.DTOs;
using JWTAppBackOffice.Core.Application.Features.CQRS.Queries;
using JWTAppBackOffice.Core.Application.Interfaces;
using JWTAppBackOffice.Core.Domain;
using MediatR;

namespace JWTAppBackOffice.Core.Application.Features.CQRS.Handlers
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQueryRequest, List<CategoryListDto>>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CategoryListDto>> Handle(GetCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await this._repository.GetAllAsync();
            return this._mapper.Map<List<CategoryListDto>>(data);
        }
    }
}
