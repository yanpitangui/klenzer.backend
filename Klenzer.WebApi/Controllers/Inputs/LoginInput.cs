using System.ComponentModel.DataAnnotations;

namespace Klenzer.WebApi.Controllers.Inputs
{
    public class LoginInput
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
