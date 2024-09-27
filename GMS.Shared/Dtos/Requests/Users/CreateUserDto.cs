using GMS.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace GMS.Shared.Dtos.Requests.Users
{
    public class CreateUserDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(450)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [EnumDataType(typeof(Roles))]
        public string Role { get; set; }

        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$", 
            ErrorMessage = "Password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, one digit, and one special character.") ]
        public string Password { get; set; }
    }
}
