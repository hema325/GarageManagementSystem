using GMS.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace GMS.Shared.Dtos.Requests.Vehicles
{
    public class CreateOrUpdateVehicleDto
    {
        [Required]
        public string LicensePlate { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [EnumDataType(typeof(VehicleStatus))]
        public string Status { get; set; }

        [Required]
        public int? OwnerId { get; set; }

        [Required]
        public int? BrandId { get; set; }
    }
}
