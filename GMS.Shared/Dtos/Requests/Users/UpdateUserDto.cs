using GMS.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace GMS.Shared.Dtos.Requests.Users
{
    public class UpdateUserDto
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
    }
}
