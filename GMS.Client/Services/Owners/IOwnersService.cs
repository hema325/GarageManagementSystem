using GMS.Shared.Dtos.Requests.Owners;
using GMS.Shared.Dtos.Responses.Global;
using GMS.Shared.Dtos.Responses.Owners;

namespace GMS.Client.Services.Owners
{
    public interface IOwnersService
    {
        Task<Response<int?>> CreateAsync(CreateOrUpdateOwnerDto dto);
        Task<Response<object>> UpdateAsync(int id, CreateOrUpdateOwnerDto dto);
        Task<Response<object>> DeleteAsync(int id);

        Task<Response<OwnerDto?>> GetByIdAsync(int id);
        Task<Response<List<OwnerDto>>> GetAllAsync();
    }
}
