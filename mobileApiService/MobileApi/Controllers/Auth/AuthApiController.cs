using System.Threading.Tasks;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MobileApi.Core.Application;
using MobileApi.Core.Services.Auth;

namespace MobileApi.Controllers.Auth
{
    [ApiController]
    [Route("api/auth")]
    public class AuthApiController : ApiController
    {
        private readonly AuthService _authService;
        private readonly string _jwtSecretKey;

        public AuthApiController(AuthService authService, ILogger<AuthApiController> logger)
        {
            _authService = authService;
            
        }
        
        [HttpGet("getToken")]    
        public async Task<IActionResult> GetToken()
        {

            return Ok();
        }
    }
}