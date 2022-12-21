using JWTAppBackOffice.Core.Application.DTOs;
using JWTAppBackOffice.Core.Application.Features.CQRS.Queries;
using JWTAppBackOffice.Core.Application.Interfaces;
using JWTAppBackOffice.Core.Domain;
using MediatR;

namespace JWTAppBackOffice.Core.Application.Features.CQRS.Handlers
{
    public class CheckUserQueryRequestHandler : IRequestHandler<CheckUserQueryRequest, CheckUserResponseDto>
    {
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly IRepository<AppRole> _appRoleRepository;

        public CheckUserQueryRequestHandler(IRepository<AppUser> appUserRepository, IRepository<AppRole> appRoleRepository)
        {
            _appUserRepository = appUserRepository;
            _appRoleRepository = appRoleRepository;
        }

        public async Task<CheckUserResponseDto> Handle(CheckUserQueryRequest request, CancellationToken cancellationToken)
        {
            var dto = new CheckUserResponseDto();

            var user = await this._appUserRepository.GetByFilterAsync(x => x.UserName == request.Username && x.Password == request.Password);

            if(user == null) { dto.IsExist = false; }
            else
            {
                dto.IsExist = true;
                dto.Username = user.UserName;
                dto.Id = user.Id;
                var role = await this._appRoleRepository.GetByFilterAsync(x => x.Id == user.AppRoleId);
                dto.Role = role?.Definition;
            }

            


            return dto;
        }
    }
}
