using System.ComponentModel.DataAnnotations;

namespace Klenzer.WebApi.Controllers.Inputs
{
    public class CreateUserInput
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "O nome do usuário é necessário.")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O username é necessário.")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A senha do usuário é necessária.")]
        public string Password { get; set; }

        /// <summary>
        /// Representa o tipo do usuário na aplicação. User(default) ou Admin.
        /// </summary>
        public string Role { get; set; } = "User";
    }
}
