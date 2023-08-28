using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.DTO;
using WebShop.Enums;
using WebShop.Exceptions;
using WebShop.Interfaces;
using WebShop.Services;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminService> _logger;
        public AdminController(IAdminService adminService, ILogger<AdminService> logger)
        {
            _adminService = adminService;
            this._logger = logger;
        }

        [HttpGet("verified-users")]
        public async Task<IActionResult> GetAllVerified()
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if(user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[GetAllVerified] [User: {user}] - Function is called.");

            if(!User.IsInRole("Admin"))
            {
                _logger.LogWarning($"[GetAllVerified] [User: {user}] - Unauthorized access. Required role: Admin");
                throw new UnauthorizedException("You are not authorized!");
            }

            if (!int.TryParse(userId, out int adminId))
            {
                _logger.LogError($"[GetAllVerified] [User: {user}] - Error with ID, Please try again.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            var users = await _adminService.GetAllVerified(adminId);
            _logger.LogInformation($"[GetAllVerified] [User: {user}] - Function is completed successfully.");
            return Ok(users);
        }

        [HttpGet("unverified-users")]
        public async Task<IActionResult> GetAllUnverified()
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[GetAllUnverified] [User: {user}] - Function is called.");

            if (!User.IsInRole("Admin"))
            {
                _logger.LogWarning($"[GetAllUnverified] [User: {user}] - Unauthorized access. Required role: Admin");
                throw new UnauthorizedException("You are not authorized!");
            }

            if (!int.TryParse(userId, out int adminId))
            {
                _logger.LogError($"[GetAllUnverified] [User: {user}] - Error with ID. Please try again.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            var users = await _adminService.GetAllUnverified(adminId);
            _logger.LogInformation($"[GetAllUnverified] [User: {user}] - Function is completed successfully.");
            return Ok(users);
        }

        [HttpGet("declined-users")]
        public async Task<IActionResult> GetAllDeclined()
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[GetAllDeclined] [User: {user}] - Function is called.");

            if (!User.IsInRole("Admin"))
            {
                _logger.LogWarning($"[GetAllDeclined] [User: {user}] - Unauthorized access. Required role: Admin");
                throw new UnauthorizedException("You are not authorized!");
            }

            if (!int.TryParse(userId, out int adminId))
            {
                _logger.LogError($"[GetAllDeclined] [User: {user}] - Error with ID. Please try again.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            var users = await _adminService.GetAllDeclined(adminId);
            _logger.LogInformation($"[GetAllDeclined] [User: {user}] - Function is completed successfully.");
            return Ok(users);
        }

        [HttpGet("buyers")]
        public async Task<IActionResult> GetAllBuyers()
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[GetAllBuyers] [User: {user}] - Function is called.");

            if (!User.IsInRole("Admin"))
            {
                _logger.LogWarning($"[GetAllBuyers] [User: {user}] - Unauthorized access. Required role: Admin");
                throw new UnauthorizedException("You are not authorized!");
            }

            if (!int.TryParse(userId, out int adminId))
            {
                _logger.LogError($"[GetAllBuyers] [User: {user}] - Error with ID. Please try again.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            var users = await _adminService.GetAllBuyers(adminId);
            _logger.LogInformation($"[GetAllBuyers] [User: {user}] - Function is completed successfully.");
            return Ok(users);
        }

        [HttpPost("verify-user")]
        public async Task<IActionResult> VerifyUser(UserVerifyDTO userVerifyDTO)
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[VerifyUser] [User: {user}] - Function is called.");

            if (!User.IsInRole("Admin"))
            {
                _logger.LogWarning($"[VerifyUser] [User: {user}] - Unauthorized access. Required role: Admin");
                throw new UnauthorizedException("You are not authorized!");
            }

            if (!int.TryParse(userId, out int adminId))
            {
                _logger.LogError($"[VerifyUser] [User: {user}] - Error with ID. Please try again.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            await _adminService.VerifyUser(userVerifyDTO, adminId);
            _logger.LogInformation($"[VerifyUser] [User: {user}] - User verification completed successfully.");
            return Ok();
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[GetAllOrders] [User: {user}] - Function is called.");

            if (!User.IsInRole("Admin"))
            {
                _logger.LogWarning($"[GetAllOrders] [User: {user}] - Unauthorized access. Required role: Admin");
                throw new UnauthorizedException("You are not authorized!");
            }

            if (!int.TryParse(userId, out int adminId))
            {
                _logger.LogError($"[GetAllOrders] [User: {user}] - Error with ID. Please try again.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            var orders = await _adminService.GetAllOrders(adminId);
            _logger.LogInformation($"[GetAllOrders] [User: {user}] - Function is completed successfully.");
            return Ok(orders);
        }
    }
}
