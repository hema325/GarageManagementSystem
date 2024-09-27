using GMS.Client.Models;
using GMS.Shared.Dtos.Requests.Brands;
using GMS.Shared.Dtos.Responses.Brands;
using GMS.Shared.Dtos.Responses.Global;

namespace GMS.Client.Services.Brands
{
    public interface IBrandsService
    {
        Task<Response<int?>> CreateAsync(CreateOrUpdateBrandModel dto);
        Task<Response<object>> UpdateAsync(int id, CreateOrUpdateBrandModel dto);
        Task<Response<object>> DeleteAsync(int id);

        Task<Response<BrandDto?>> GetByIdAsync(int id);
        Task<Response<List<BrandDto>>> GetAllAsync();
    }
}
