using System.ComponentModel.DataAnnotations;
using WebShop.Enums;

namespace WebShop.Models
{
    public class Product : EntityBase
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public double Price { get; set; }
        [Required]
        public int Amount { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public User? Seller { get; set; }
        [Required]
        public int SellerId { get; set; }
        [Required]
        public ProductCategory Category { get; set; }
    }
}
