using GMS.API.Data.Repositories.Brands;
using GMS.API.Data.Repositories.Owners;
using GMS.API.Data.Repositories.Users;
using GMS.API.Data.Repositories.Vehicles;

namespace GMS.API.Data.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUsersRepository Users { get; }
        IBrandsRepository Brands { get; }
        IOwnerRepository Owners { get; }
        IVehicleRepository Vehicles { get; }

        Task SaveChangesAsync();
    }
}
