using GMS.Shared.Dtos.Responses.Brands;
using GMS.Shared.Dtos.Responses.Owners;

namespace GMS.Shared.Dtos.Responses.Vehicles
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public OwnerDto Owner { get; set; }
        public BrandDto Brand { get; set; }
    }
}
