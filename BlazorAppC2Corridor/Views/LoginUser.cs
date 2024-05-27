using System.ComponentModel.DataAnnotations;

namespace BlazorAppC2Corridor.Views
{
    public class LoginUser
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
