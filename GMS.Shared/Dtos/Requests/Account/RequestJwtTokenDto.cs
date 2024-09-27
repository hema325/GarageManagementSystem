using System.ComponentModel.DataAnnotations;

namespace GMS.Shared.Dtos.Requests.Account
{
    public class RequestJwtTokenDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
