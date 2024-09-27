using GMS.Client.Models;
using GMS.Shared.Dtos.Requests.Brands;
using GMS.Shared.Dtos.Responses.Brands;
using GMS.Shared.Dtos.Responses.Global;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GMS.Client.Services.Brands
{
    public class BrandsService: IBrandsService
    {
        private readonly HttpClient _httpClient;

        public BrandsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<int?>> CreateAsync(CreateOrUpdateBrandModel dto)
        {
            var form = new MultipartFormDataContent();
            form.Add(new StringContent(dto.Name), nameof(CreateOrUpdateBrandDto.Name));

            if (dto.Image != null)
            {
                var streamContent = new StreamContent(dto.Image.OpenReadStream(long.MaxValue));
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(dto.Image.ContentType);

                form.Add(streamContent, nameof(CreateOrUpdateBrandDto.Image), dto.Image.Name);
            }

            var responseMessage = await _httpClient.PostAsync(Router.Brands.Create, form);

            var response = await responseMessage.Content.ReadFromJsonAsync<Response<int?>>();

            return response!;
        }

        public async Task<Response<object>> UpdateAsync(int id, CreateOrUpdateBrandModel dto)
        {
            var form = new MultipartFormDataContent();
            form.Add(new StringContent(dto.Name), nameof(CreateOrUpdateBrandDto.Name));

            if (dto.Image != null)
            {
                var streamContent = new StreamContent(dto.Image.OpenReadStream(long.MaxValue));
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(dto.Image.ContentType);

                form.Add(streamContent, nameof(CreateOrUpdateBrandDto.Image), dto.Image.Name);
            }

            var responseMessage = await _httpClient.PutAsync(Router.Brands.Update.Replace("{id}", id.ToString()), form);

            var response = await responseMessage.Content.ReadFromJsonAsync<Response<object>>();

            return response!;
        }

        public async Task<Response<object>> DeleteAsync(int id)
        {
            var responseMessage = await _httpClient.DeleteAsync(Router.Brands.Delete.Replace("{id}", id.ToString()));

            var response = await responseMessage.Content.ReadFromJsonAsync<Response<object>>();

            return response!;
        }

        public async Task<Response<BrandDto?>> GetByIdAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync(Router.Brands.GetById.Replace("{id}", id.ToString()));

            var response = await responseMessage.Content.ReadFromJsonAsync<Response<BrandDto?>>();

            return response!;
        }
        
        public async Task<Response<List<BrandDto>>> GetAllAsync()
        {
            var responseMessage = await _httpClient.GetAsync(Router.Brands.GetAll);

            var response = await responseMessage.Content.ReadFromJsonAsync<Response<List<BrandDto>>>();

            return response!;
        }
    }
}
