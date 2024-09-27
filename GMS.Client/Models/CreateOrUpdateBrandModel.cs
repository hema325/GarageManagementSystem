using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace GMS.Client.Models
{
    public class CreateOrUpdateBrandModel
    {
        [Required]
        public string Name { get; set; }

        public IBrowserFile? Image { get; set; }
    }
}
