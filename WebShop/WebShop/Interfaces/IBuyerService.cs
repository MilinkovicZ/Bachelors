using WebShop.DTO;
using WebShop.Models;

namespace WebShop.Interfaces
{
    public interface IBuyerService
    {
        Task<List<ProductDTO>> GetAllProducts(int buyerId);
        Task CreateOrder(CreateOrderDTO orderDTO, int buyerId);
        Task DeclineOrder(int orderId, int buyerId);
        Task<List<OrderDTO>> GetMyOrders(int buyerId);
        Task<double> GetTotalPrice(List<CreateItemDTO> items, int buyerId);
    }
}
