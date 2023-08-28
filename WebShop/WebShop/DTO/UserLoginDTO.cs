using System.ComponentModel.DataAnnotations;

namespace WebShop.DTO
{
    public class UserLoginDTO
    {
        [Required]
        [MaxLength(30)]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        public string Password { get; set; } = null!;
    }
}
