using GMS.Shared.Dtos.Requests.Vehicles;
using GMS.Shared.Dtos.Responses.Vehicles;

namespace GMS.API.Mappings.Profiles
{
    public class VehiclesProfile: Profile
    {
        public VehiclesProfile()
        {
            CreateMap<CreateOrUpdateVehicleDto, Vehicle>();
            CreateMap<Vehicle, VehicleDto>();
        }
    }
}
