using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GMS.Shared.Dtos.Requests.Brands
{
    public class CreateOrUpdateBrandDto
    {
        [Required]
        public string Name { get; set; }

        public IFormFile? Image { get; set; }
    }
}
