using GMS.Shared.Dtos.Requests.Owners;
using GMS.Shared.Dtos.Responses.Global;
using GMS.Shared.Dtos.Responses.Owners;
using GMS.Shared.Dtos.Responses.Users;
using System.Net.Http.Json;

namespace GMS.Client.Services.Owners
{
    public class OwnersService: IOwnersService
    {
        private readonly HttpClient _httpClient;

        public OwnersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<int?>> CreateAsync(CreateOrUpdateOwnerDto dto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync(Router.Owners.Create, dto);

            var response = await responseMessage.Content.ReadFromJsonAsync<Response<int?>>();

            return response!;
        }

        public async Task<Response<object>> UpdateAsync(int id, CreateOrUpdateOwnerDto dto)
        {
            var responseMessage = await _httpClient.PutAsJsonAsync(Router.Owners.Update.Replace("{id}", id.ToString()), dto);

            var response = await responseMessage.Content.ReadFromJsonAsync<Response<object>>();

            return response!;
        }

        public async Task<Response<object>> DeleteAsync(int id)
        {
            var responseMessage = await _httpClient.DeleteAsync(Router.Owners.Delete.Replace("{id}", id.ToString()));

            var response = await responseMessage.Content.ReadFromJsonAsync<Response<object>>();

            return response!;
        }

        public async Task<Response<OwnerDto?>> GetByIdAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync(Router.Owners.GetById.Replace("{id}", id.ToString()));

            var response = await responseMessage.Content.ReadFromJsonAsync<Response<OwnerDto>>();

            return response!;
        }


        public async Task<Response<List<OwnerDto>>> GetAllAsync()
        {
            var responseMessage = await _httpClient.GetAsync(Router.Owners.GetAll);

            var response = await responseMessage.Content.ReadFromJsonAsync<Response<List<OwnerDto>>>();

            return response!;
        }
    }
}
