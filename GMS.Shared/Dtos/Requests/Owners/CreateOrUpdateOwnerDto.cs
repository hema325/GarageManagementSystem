using System.ComponentModel.DataAnnotations;

namespace GMS.Shared.Dtos.Requests.Owners
{
    public class CreateOrUpdateOwnerDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }
    }
}
