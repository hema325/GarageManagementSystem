using GMS.API.Data.Repositories._BaseRepository;

namespace GMS.API.Data.Repositories.Owners
{
    public class OwnerRepository : BaseRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
