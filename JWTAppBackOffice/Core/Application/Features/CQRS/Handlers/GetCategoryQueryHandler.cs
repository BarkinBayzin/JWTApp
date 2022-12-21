using AutoMapper;
using JWTAppBackOffice.Core.Application.DTOs;
using JWTAppBackOffice.Core.Application.Features.CQRS.Queries;
using JWTAppBackOffice.Core.Application.Interfaces;
using JWTAppBackOffice.Core.Domain;
using MediatR;

namespace JWTAppBackOffice.Core.Application.Features.CQRS.Handlers
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQueryRequest, CategoryListDto>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CategoryListDto> Handle(GetCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await this._repository.GetByFilterAsync(x => x.Id == request.Id);
            return this._mapper.Map<CategoryListDto>(data);
        }
    }
}
