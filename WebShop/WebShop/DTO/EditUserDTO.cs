using System.ComponentModel.DataAnnotations;
using WebShop.Enums;

namespace WebShop.DTO
{
    public class EditUserDTO
    {
        [Required]
        [MaxLength(20)]
        [RegularExpression("[a-zA-Z0-9]+")]
        public string Username { get; set; } = null!;
        [MaxLength(30)]
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [MaxLength(30)]
        [Required]
        public string Password { get; set; } = null!;
        [MaxLength(30)]
        public string? NewPassword { get; set; }
        [MaxLength(30)]
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public DateTime BirthDate { get; set; }
        [MaxLength(40)]
        [Required]
        public string Address { get; set; } = null!;
    }
}
