using GMS.Shared.Enums;

namespace GMS.Shared.Dtos.Requests.Vehicles
{
    public class FilterVehicleDto
    {
        public string? LicensePlate { get; set; }

        public VehicleStatus? Status { get; set; }

        public int? OwnerId { get; set; }

        public int? BrandId { get; set; }
    }
}
