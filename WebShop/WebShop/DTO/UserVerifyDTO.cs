using System.ComponentModel.DataAnnotations;
using WebShop.Enums;

namespace WebShop.DTO
{
    public class UserVerifyDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public VerificationState verificationState { get; set; }
    }
}
