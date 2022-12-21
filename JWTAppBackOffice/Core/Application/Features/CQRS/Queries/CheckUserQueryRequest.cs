using JWTAppBackOffice.Core.Application.DTOs;
using MediatR;

namespace JWTAppBackOffice.Core.Application.Features.CQRS.Queries
{
    public class CheckUserQueryRequest : IRequest<CheckUserResponseDto>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
