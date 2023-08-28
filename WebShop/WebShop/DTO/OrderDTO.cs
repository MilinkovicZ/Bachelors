using System.ComponentModel.DataAnnotations;
using WebShop.Enums;

namespace WebShop.DTO
{
    public class CreateOrderDTO
    {
        public List<CreateItemDTO> Items { get; set; } = new List<CreateItemDTO>();
        [Required]
        public string DeliveryAddress { get; set; } = null!;
        public string? Comment { get; set; }
    }

    public class OrderDTO
    {
        public List<ItemDTO> Items { get; set; } = new List<ItemDTO>();
        [Required]
        public string DeliveryAddress { get; set; } = null!;
        public string? Comment { get; set; }
        [Required]
        public int Id { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public OrderState OrderState { get; set; }
        [Required]
        public bool IsAccepted { get; set; }
    }
}
