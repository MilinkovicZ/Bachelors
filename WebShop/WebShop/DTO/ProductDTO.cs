using System.ComponentModel.DataAnnotations;
using WebShop.Enums;

namespace WebShop.DTO
{
    public class CreateProductDTO
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public double Price { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Amount { get; set; } 
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public IFormFile? ImageForm { get; set; }
        [Required]
        public ProductCategory Category { get; set; }
    }

    public class ProductDTO : CreateProductDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int SellerId { get; set; }
    }
}
