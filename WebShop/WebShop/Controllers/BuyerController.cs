using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.DTO;
using WebShop.Exceptions;
using WebShop.Interfaces;
using WebShop.Services;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IBuyerService _buyerService;
        private readonly ILogger<BuyerService> _logger;
        public BuyerController(IBuyerService buyerService, ILogger<BuyerService> logger)
        {
            _buyerService = buyerService;
            this._logger = logger;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[GetAllProducts] [User: {user}] - Function is called.");

            if (!User.IsInRole("Buyer"))
            {
                _logger.LogWarning($"[GetAllProducts] [User: {user}] - Unauthorized access. Required role: Buyer");
                throw new UnauthorizedException("You are not authorized!");
            }

            if (!int.TryParse(userId, out int buyerId))
            {
                _logger.LogError($"[GetAllProducts] [User: {user}] - Error with BuyerID. Please try again.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            var products = await _buyerService.GetAllProducts(buyerId);
            _logger.LogInformation($"[GetAllProducts] [User: {user}] - Function is completed successfully.");
            return Ok(products);
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[GetMyOrders] [User: {user}] - Function is called.");

            if (!User.IsInRole("Buyer"))
            {
                _logger.LogWarning($"[GetMyOrders] [User: {user}] - Unauthorized access. Required role: Buyer");
                throw new UnauthorizedException("You are not authorized!");
            }

            if (!int.TryParse(userId, out int buyerId))
            {
                _logger.LogError($"[GetMyOrders] [User: {user}] - Error with BuyerID. Please try again.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            var orders = await _buyerService.GetMyOrders(buyerId);
            _logger.LogInformation($"[GetMyOrders] [User: {user}] - Function is completed successfully.");
            return Ok(orders);
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO orderDTO)
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[CreateOrder] [User: {user}] - Function is called.");

            if (!User.IsInRole("Buyer"))
            {
                _logger.LogWarning($"[CreateOrder] [User: {user}] - Unauthorized access. Required role: Buyer");
                throw new UnauthorizedException("You are not authorized!");
            }

            if (!int.TryParse(userId, out int buyerId))
            {
                _logger.LogError($"[CreateOrder] [User: {user}] - Error with BuyerID. Please try again.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            await _buyerService.CreateOrder(orderDTO, buyerId);
            _logger.LogInformation($"[CreateOrder] [User: {user}] - Order creation completed successfully.");
            return Ok();
        }

        [HttpPost("decline-order/{id}")]
        public async Task<IActionResult> DeclineOrder(int id)
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[DeclineOrder] [User: {user}] - Function is called.");

            if (!User.IsInRole("Buyer"))
            {
                _logger.LogWarning($"[DeclineOrder] [User: {user}] - Unauthorized access. Required role: Buyer");
                throw new UnauthorizedException("You are not authorized!");
            }

            if (!int.TryParse(userId, out int buyerId))
            {
                _logger.LogError($"[DeclineOrder] [User: {user}] - Error with BuyerID. Please try again.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            await _buyerService.DeclineOrder(id, buyerId);
            _logger.LogInformation($"[DeclineOrder] [User: {user}] - Order decline completed successfully.");
            return Ok();
        }

        [HttpPost("price")]
        public async Task<IActionResult> GetTotalPrice([FromBody] List<CreateItemDTO> items)
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[GetTotalPrice] [User: {user}] - Function is called.");

            if (!User.IsInRole("Buyer"))
            {
                _logger.LogWarning($"[GetTotalPrice] [User: {user}] - Unauthorized access. Required role: Buyer");
                throw new UnauthorizedException("You are not authorized!");
            }

            if (!int.TryParse(userId, out int buyerId))
            {
                _logger.LogError($"[GetTotalPrice] [User: {user}] - Error with BuyerID. Please try again.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            double totalPrice = await _buyerService.GetTotalPrice(items, buyerId);
            _logger.LogInformation($"[GetTotalPrice] [User: {user}] - Function is completed successfully.");
            return Ok(totalPrice);
        }
    }
}
