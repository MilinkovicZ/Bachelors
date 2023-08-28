using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebShop.DTO;
using WebShop.Exceptions;
using WebShop.Interfaces;
using WebShop.Services;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerService;
        private readonly ILogger<SellerService> _logger;
        public SellerController(ISellerService sellerService, ILogger<SellerService> logger)
        {
            _sellerService = sellerService;
            this._logger = logger;
        }

        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[GetProduct] [User: {user}] - Function is called.");

            if (!User.IsInRole("Seller"))
            {
                _logger.LogWarning($"[GetProduct] [User: {user}] - Unauthorized access. Required role: Seller");
                throw new UnauthorizedException("You are not authorized!");
            }

            if (!int.TryParse(userId, out int sellerId))
            {
                _logger.LogError($"[GetProduct] [User: {user}] - Error with SellerID. Please try again.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            var product = await _sellerService.GetProduct(id, sellerId);
            _logger.LogInformation($"[GetProduct] [User: {user}] - Function is completed successfully.");
            return Ok(product);
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[GetAllProducts] [User: {user}] - Function is called.");

            if (!User.IsInRole("Seller"))
            {
                _logger.LogWarning($"[GetAllProducts] [User: {user}] - Unauthorized access. Required role: Seller");
                throw new UnauthorizedException("You are not authorized!");
            }

            if (!int.TryParse(userId, out int sellerId))
            {
                _logger.LogError($"[GetAllProducts] [User: {user}] - Error with SellerID. Please try again.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            var products = await _sellerService.GetAllProducts(sellerId);
            _logger.LogInformation($"[GetAllProducts] [User: {user}] - Function is completed successfully.");
            return Ok(products);
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[GetAllOrders] [User: {user}] - Function is called.");

            if (!User.IsInRole("Seller"))
            {
                _logger.LogWarning($"[GetAllOrders] [User: {user}] - Unauthorized access. Required role: Seller");
                throw new UnauthorizedException("You are not authorized!");
            }

            if (!int.TryParse(userId, out int sellerId))
            {
                _logger.LogError($"[GetAllOrders] [User: {user}] - Error with SellerID. Please try again.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            var orders = await _sellerService.GetAllOrders(sellerId);
            _logger.LogInformation($"[GetAllOrders] [User: {user}] - Function is completed successfully.");
            return Ok(orders);
        }

        [HttpGet("new-orders")]
        public async Task<IActionResult> GetNewOrders()
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[GetNewOrders] [User: {user}] - Function is called.");

            if (!User.IsInRole("Seller"))
            {
                _logger.LogWarning($"[GetNewOrders] [User: {user}] - Unauthorized access. Required role: Seller");
                throw new UnauthorizedException("You are not authorized!");
            }

            if (!int.TryParse(userId, out int sellerId))
            {
                _logger.LogError($"[GetNewOrders] [User: {user}] - Error with SellerID. Please try again.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            var orders = await _sellerService.GetNewOrders(sellerId);
            _logger.LogInformation($"[GetNewOrders] [User: {user}] - Function is completed successfully.");
            return Ok(orders);
        }

        [HttpPost("products")]
        public async Task<IActionResult> CreateNewProduct([FromForm] CreateProductDTO productDTO)
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[CreateNewProduct] [User: {user}] - Function is called.");

            if (!User.IsInRole("Seller"))
            {
                _logger.LogWarning($"[CreateNewProduct] [User: {user}] - Unauthorized access. Required role: Seller");
                throw new UnauthorizedException("You are not authorized!");
            }

            if (!int.TryParse(userId, out int sellerId))
            {
                _logger.LogError($"[CreateNewProduct] [User: {user}] - Error with SellerID. Please try again.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            await _sellerService.CreateNewProduct(productDTO, sellerId);
            _logger.LogInformation($"[CreateNewProduct] [User: {user}] - Product creation completed successfully.");
            return Ok();
        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[DeleteProduct] [User: {user}] - Function is called.");

            if (!User.IsInRole("Seller"))
            {
                _logger.LogWarning($"[DeleteProduct] [User: {user}] - Unauthorized access. Required role: Seller");
                throw new UnauthorizedException("You are not authorized!");
            }

            if (!int.TryParse(userId, out int sellerId))
            {
                _logger.LogError($"[DeleteProduct] [User: {user}] - Error with SellerID. Please try again.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            await _sellerService.DeleteProduct(id, sellerId);
            _logger.LogInformation($"[DeleteProduct] [User: {user}] - Product deletion completed successfully.");
            return Ok();
        }

        [HttpPut("products/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] CreateProductDTO productDTO)
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[UpdateProduct] [User: {user}] - Function is called.");

            if (!User.IsInRole("Seller"))
            {
                _logger.LogWarning($"[UpdateProduct] [User: {user}] - Unauthorized access. Required role: Seller");
                throw new UnauthorizedException("You are not authorized!");
            }

            if (!int.TryParse(userId, out int sellerId))
            {
                _logger.LogError($"[UpdateProduct] [User: {user}] - Error with SellerID. Please try again.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            await _sellerService.UpdateProduct(id, productDTO, sellerId);
            _logger.LogInformation($"[UpdateProduct] [User: {user}] - Product update completed successfully.");
            return Ok();
        }

        [HttpPost("accept-order/{id}")]
        public async Task<IActionResult> AcceptOrder(int id)
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (user == null) { user = "unknown"; }
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _logger.LogInformation($"[AcceptOrder] [User: {user}] - Function is called.");

            if (!User.IsInRole("Seller"))
            {
                _logger.LogWarning($"[AcceptOrder] [User: {user}] - Unauthorized access. Required role: Seller");
                throw new UnauthorizedException("You are not authorized!");
            }

            if (!int.TryParse(userId, out int sellerId))
            {
                _logger.LogError($"[AcceptOrder] [User: {user}] - Error with SellerID. Please try again.");
                throw new BadRequestException("Error with ID. Please try again.");
            }

            await _sellerService.AcceptOrder(id, sellerId);
            _logger.LogInformation($"[AcceptOrder] [User: {user}] - Order acceptance completed successfully.");
            return Ok();
        }
    }
}
