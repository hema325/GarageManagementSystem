using GMS.API.Data.Repositories._BaseRepository;
using GMS.Shared.Dtos.Requests.Vehicles;

namespace GMS.API.Data.Repositories.Vehicles
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        Task<List<Vehicle>> FilterIncludeRelatedAsync(FilterVehicleDto filter);
        Task<Vehicle?> GetByIdIncludeRelatedAsync(int id);
    }
}
