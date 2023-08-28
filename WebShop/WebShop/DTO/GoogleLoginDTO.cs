using System.ComponentModel.DataAnnotations;

namespace WebShop.DTO
{
    public class GoogleLoginDTO
    {
        [Required]
        public string? Token { get; set; }
    }
}
