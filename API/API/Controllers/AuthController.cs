using Business.DTOs.Auth;
using Business.DTOs.Token;
using Business.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto login)
        {
            TokenDto token=  await _authService.LoginJwt(login.UsernameOrEmail, login.Password);
            return Ok(token);
        }
        [HttpPost("refresh-token-login")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody]RefreshTokenLoginDto login)
        {
            TokenDto token = await _authService.RefreshTokenLogin(login.RefreshToken);
            return Ok(token);
        }
    }
}
