using System.ComponentModel.DataAnnotations;

namespace MyEshop.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(300)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }

    }

    public class LoginViewModel
    {
        [Required]
        [MaxLength(300)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}
