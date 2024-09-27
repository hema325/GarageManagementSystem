using GMS.Shared.Dtos.Requests.Users;
using GMS.Shared.Dtos.Responses.Global;
using GMS.Shared.Dtos.Responses.Users;
using System.Net.Http.Json;

namespace GMS.Client.Services.Users
{
    public class UsersService: IUsersService
    {
        private readonly HttpClient _httpClient;

        public UsersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<int?>> CreateAsync(CreateUserDto dto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync(Router.Users.Create, dto);
             
            var response = await responseMessage.Content.ReadFromJsonAsync<Response<int?>>();

            return response!;
        }

        public async Task<Response<object>> UpdateAsync(int id, UpdateUserDto dto)
        {
            var responseMessage = await _httpClient.PutAsJsonAsync(Router.Users.Update.Replace("{id}", id.ToString()), dto);

            var response = await responseMessage.Content.ReadFromJsonAsync<Response<object>>();

            return response!;
        }

        public async Task<Response<object>> DeleteAsync(int id)
        {
            var responseMessage = await _httpClient.DeleteAsync(Router.Users.Delete.Replace("{id}", id.ToString()));

            var response = await responseMessage.Content.ReadFromJsonAsync<Response<object>>();

            return response!;
        }

        public async Task<Response<UserDto?>> GetByIdAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync(Router.Users.GetById.Replace("{id}", id.ToString()));

            var response = await responseMessage.Content
                .ReadFromJsonAsync<Response<UserDto?>>();

            return response!;
        }

        public async Task<Response<List<UserDto>>> GetAllAsync()
        {
            var responseMessage = await _httpClient.GetAsync(Router.Users.GetAll);

            var response = await responseMessage.Content
                .ReadFromJsonAsync<Response<List<UserDto>>>();

            return response!;
        }
    }
}
