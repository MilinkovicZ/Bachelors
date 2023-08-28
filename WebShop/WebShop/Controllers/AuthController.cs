using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebShop.DTO;
using WebShop.Interfaces;
using WebShop.Models;
using WebShop.Services;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthService> _logger;
        public AuthController(IAuthService authService, ILogger<AuthService> logger)
        {
            _authService = authService;
            this._logger = logger;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            string token = await _authService.Login(userLoginDTO);
            _logger.LogInformation($"[Login] [User: {userLoginDTO.Email}] Has logged in successfully.");
            return Ok(new { token = token });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
        {
            await _authService.Register(userRegisterDTO);
            _logger.LogInformation($"[Register] [User: {userRegisterDTO.Email}] Has registered in successfully.");
            return Ok();
        }

        [HttpPost("register-via-google")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterViaGoogle(GoogleLoginDTO googleLoginDTO)
        {
            var token = await _authService.RegisterViaGoogle(googleLoginDTO);
            _logger.LogInformation($"[RegisterViaGoogle] [User: {googleLoginDTO.Token}] Has registered via google successfully.");
            return Ok(new {token = token});
        }
    }
}