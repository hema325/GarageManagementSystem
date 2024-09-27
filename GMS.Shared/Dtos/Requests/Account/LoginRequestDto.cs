using System.ComponentModel.DataAnnotations;

namespace GMS.Shared.Dtos.Requests.Account
{
    public class LoginRequestDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
