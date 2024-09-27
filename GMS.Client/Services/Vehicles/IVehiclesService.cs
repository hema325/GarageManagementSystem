using GMS.Shared.Dtos.Requests.Vehicles;
using GMS.Shared.Dtos.Responses.Global;
using GMS.Shared.Dtos.Responses.Vehicles;

namespace GMS.Client.Services.Vehicles
{
    public interface IVehiclesService
    {
        Task<Response<int?>> CreateAsync(CreateOrUpdateVehicleDto dto);
        Task<Response<object>> UpdateAsync(int id, CreateOrUpdateVehicleDto dto);
        Task<Response<object>> DeleteAsync(int id);

        Task<Response<VehicleDto?>> GetByIdAsync(int id);
        Task<Response<List<VehicleDto>>> GetAllAsync(FilterVehicleDto? filter = null);
        Task<Response<object>> UpdateStatusAsync(int id, VehicleStatus status);
    }
}
