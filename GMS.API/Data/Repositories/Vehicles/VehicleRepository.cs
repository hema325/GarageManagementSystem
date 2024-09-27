using GMS.API.Data.Repositories._BaseRepository;
using GMS.Shared.Dtos.Requests.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace GMS.API.Data.Repositories.Vehicles
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<Vehicle?> GetByIdIncludeRelatedAsync(int id)
        { 
            return _context.Vehicles
                .Include(v => v.Owner)
                .Include(v => v.Brand)
                .FirstOrDefaultAsync(v => v.Id == id); 
        }
        
        public Task<List<Vehicle>> FilterIncludeRelatedAsync(FilterVehicleDto filter)
        {
            var query = _context.Vehicles.AsQueryable();

            if(filter.OwnerId != null)
                query = query.Where(v=>v.OwnerId == filter.OwnerId);

            if(filter.BrandId != null)
                query = query.Where(v=>v.BrandId == filter.BrandId);

            if(filter.LicensePlate != null)
                query = query.Where(v=>v.LicensePlate.StartsWith(filter.LicensePlate));

            if (filter.Status != null)
                query = query.Where(v => v.Status == filter.Status);

            return query
                .Include(v => v.Owner)
                .Include(v => v.Brand)
                .ToListAsync(); 
        }
    }
}
