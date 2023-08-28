using System.ComponentModel.DataAnnotations;
using WebShop.Enums;

namespace WebShop.Models
{
    public class User : EntityBase
    {
        [Required, RegularExpression("[a-zA-Z0-9]+")]
        public string Username { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public UserType UserType { get; set; }
        [Required]
        public VerificationState VerificationState { get; set; }
        [Required]
        public bool HasFullAccess { get; set; }
        [Required]
        public DateTime RegistrationTime { get; set; }
        public List<Order>? Orders { get; set; }
        public List<Product>? Products { get; set; }
        public byte[]? Image { get; set; }
    }
}
