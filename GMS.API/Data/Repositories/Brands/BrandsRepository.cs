using GMS.API.Data.Repositories._BaseRepository;

namespace GMS.API.Data.Repositories.Brands
{
    public class BrandsRepository : BaseRepository<Brand>, IBrandsRepository
    {
        public BrandsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
