using System.ComponentModel.DataAnnotations;

namespace PM_AUTH.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
