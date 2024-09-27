using GMS.API.Data.Repositories._BaseRepository;
using GMS.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GMS.API.Data.Repositories.Users
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<User?> GetByEmailAsync(string email)
            => _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public Task<bool> IsEmailExistsAsync(string email) 
            => _context.Users.AnyAsync(u=>u.Email == email);
        
        public Task<bool> IsEmailExistsExceptUserAsync(int userId, string email) 
            => _context.Users.AnyAsync(u=> u.Id != userId && u.Email == email);
    }
}
