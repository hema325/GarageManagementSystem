using GMS.API.Data.Repositories.Brands;
using GMS.API.Data.Repositories.Owners;
using GMS.API.Data.Repositories.Users;
using GMS.API.Data.Repositories.Vehicles;

namespace GMS.API.Data.Repositories.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Users = new UsersRepository(context);
            Brands = new BrandsRepository(context);
            Owners = new OwnerRepository(context);
            Vehicles = new VehicleRepository(context);
        }

        public IUsersRepository Users { get; }
        public IBrandsRepository Brands { get; }
        public IOwnerRepository Owners { get; }
        public IVehicleRepository Vehicles { get; }

        public Task SaveChangesAsync() 
            => _context.SaveChangesAsync();
    }
}
