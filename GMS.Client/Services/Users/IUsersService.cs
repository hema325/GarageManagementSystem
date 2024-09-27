using GMS.Shared.Dtos.Requests.Users;
using GMS.Shared.Dtos.Responses.Global;
using GMS.Shared.Dtos.Responses.Users;

namespace GMS.Client.Services.Users
{
    public interface IUsersService
    {
        Task<Response<int?>> CreateAsync(CreateUserDto dto);
        Task<Response<object>> UpdateAsync(int id, UpdateUserDto dto);
        Task<Response<object>> DeleteAsync(int id);

        Task<Response<UserDto?>> GetByIdAsync(int id);
        Task<Response<List<UserDto>>> GetAllAsync();
    }
}
