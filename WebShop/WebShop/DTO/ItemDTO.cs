using System.ComponentModel.DataAnnotations;

namespace WebShop.DTO
{
    public class CreateItemDTO
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int ProductId { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int ProductAmount { get; set; }
    }

    public class ItemDTO : CreateItemDTO
    {
        public double CurrentPrice { get; set; }
        public string? Name { get; set; }
    }
}