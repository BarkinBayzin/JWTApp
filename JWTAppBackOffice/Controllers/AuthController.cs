using JWTAppBackOffice.Core.Application.Features.CQRS.Commands;
using JWTAppBackOffice.Core.Application.Features.CQRS.Queries;
using JWTAppBackOffice.Infrastructure.Tools;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace JWTAppBackOffice.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /*
        1 - User Register => member rolü ile beraber register edilecek
        2 - username, passord doğruysa token üreteceğim.
        */

        // api/Auth/Register
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterUserCommandRequest request)
        {
            await this._mediator.Send(request);
            return Created("", request);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SignIn(CheckUserQueryRequest request)
        {
           var userDto= await this._mediator.Send(request);
            if(userDto.IsExist)
            {
                //kullanıcı var, artık token yaratmalıyım.
                var token =  JwtTokenGenerator.GenerateToken(userDto);
                return Created("", token);
            }

            return BadRequest("Username or password is invalid");
        }
    }
}
