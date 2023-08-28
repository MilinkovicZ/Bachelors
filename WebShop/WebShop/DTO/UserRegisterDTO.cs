using System.ComponentModel.DataAnnotations;
using WebShop.Enums;

namespace WebShop.DTO
{
    public class UserRegisterDTO
    {
        [Required]
        [MaxLength(20)]
        [RegularExpression("[a-zA-Z0-9]+")]
        public string Username { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        public string Password { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        public string ConfirmPassword { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        public string FullName { get; set; } = null!;
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [MaxLength(40)]
        public string Address { get; set; } = null!;
        [Required]
        public UserType UserType { get; set; }
    }
}
