using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MobileApi.Controllers.User.Dto;
using MobileApi.Core.Application;
using MobileApi.Core.Services.Auth;
using MobileApi.Core.Services.User;

namespace MobileApi.Controllers.User
{
    [ApiController]
    [Route("api/user")]
    public class UserApiController : ApiController
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;
        private readonly string _jwtSecretKey;

        public UserApiController(UserService userService, AuthService authService,  IOptions<AppConfig> options)
        {
            _userService = userService;
            _authService = authService;
            _jwtSecretKey = options.Value.JWTSecretKey;
        }
        
        [HttpGet("getUserProfile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var userProfile = await _userService.GetUserProfile();
            return ApiResponseResult(userProfile);
        }

        [HttpPost("auth")]
        public async Task<IActionResult> Auth([FromBody] UserLoginPassDto userLoginPassDto)
        {
            if (userLoginPassDto == null)
                return BadRequest("Некорректные данные");
            
            var jwtToken = _authService.GetToken(_jwtSecretKey, userLoginPassDto.UserName, userLoginPassDto.UserPassword);
            return ApiResponseResult(jwtToken);
        }
    }
}