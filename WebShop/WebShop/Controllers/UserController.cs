using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using WebShop.DTO;
using WebShop.Exceptions;
using WebShop.Interfaces;
using WebShop.Models;
using WebShop.Services;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserService> _logger;
        public UserController(IUserService userService, ILogger<UserService> logger)
        {
            _userService = userService;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[Get] [User: {user}] - Function is called.");

            if (!int.TryParse(userId, out int userIdValue))
            {
                _logger.LogError($"[Get] [User: {user}] - Error with userID. Not Authenticated.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            var userProfile = await _userService.GetUserProfile(userIdValue);

            _logger.LogInformation($"[Get] [User: {user}] - Function is completed successfully.");
            return Ok(userProfile);
        }

        [HttpPut]
        public async Task<IActionResult> EditUserProfile(EditUserDTO editUserDTO)
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[EditUserProfile] [User: {user}] - Function is called.");

            if (!int.TryParse(userId, out int userIdValue))
            {
                _logger.LogError($"[EditUserProfile] [User: {user}] - Error with userID. Not Authenticated.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            await _userService.EditUserProfile(userIdValue, editUserDTO);
            _logger.LogInformation($"[EditUserProfile] [User: {user}] - Profile edit completed successfully.");
            return Ok();
        }

        [HttpPut("add-picture")]
        public async Task<IActionResult> AddPicture([FromForm] IFormFile image)
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[AddPicture] [User: {user}] - Function is called.");

            if (!int.TryParse(userId, out int userIdValue))
            {
                _logger.LogError($"[AddPicture] [User: {user}] - Error with userID. Not Authenticated.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            await _userService.AddPicture(userIdValue, image);
            _logger.LogInformation($"[AddPicture] [User: {user}] - Adding photo completed successfully.");
            return Ok();
        }
    }
}
