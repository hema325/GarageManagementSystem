using GMS.API.Data.Repositories._BaseRepository;
using GMS.API.Entities;

namespace GMS.API.Data.Repositories.Users
{
    public interface IUsersRepository : IBaseRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> IsEmailExistsAsync(string email);
        Task<bool> IsEmailExistsExceptUserAsync(int userId, string email);
    }
}
