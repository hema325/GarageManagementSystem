using GMS.Shared.Dtos.Requests.Vehicles;
using GMS.Shared.Dtos.Responses.Global;
using GMS.Shared.Dtos.Responses.Vehicles;
using System.Net.Http.Json;

namespace GMS.Client.Services.Vehicles
{
    public class VehiclesService: IVehiclesService
    {
        private readonly HttpClient _httpClient;

        public VehiclesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<int?>> CreateAsync(CreateOrUpdateVehicleDto dto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync(Router.Vehicles.Create, dto);

            var response = await responseMessage.Content.ReadFromJsonAsync<Response<int?>>();

            return response!;
        }

        public async Task<Response<object>> UpdateAsync(int id, CreateOrUpdateVehicleDto dto) 
        {
            var responseMessage = await _httpClient.PutAsJsonAsync(Router.Vehicles.Update.Replace("{id}", id.ToString()), dto);

            var response = await responseMessage.Content.ReadFromJsonAsync<Response<object>>();

            return response!;
        } 
        
        public async Task<Response<object>> UpdateStatusAsync(int id, VehicleStatus status) 
        {
            var responseMessage = await _httpClient.PutAsync(Router.Vehicles.UpdateStatus.Replace("{id}", id.ToString()).Replace("{status}", status.ToString()), null);

            var response = await responseMessage.Content.ReadFromJsonAsync<Response<object>>();

            return response!;
        }

        public async Task<Response<object>> DeleteAsync(int id)
        {
            var responseMessage = await _httpClient.DeleteAsync(Router.Vehicles.Delete.Replace("{id}", id.ToString()));

            var response = await responseMessage.Content.ReadFromJsonAsync<Response<object>>();

            return response!;
        }

        public async Task<Response<VehicleDto?>> GetByIdAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync(Router.Vehicles.GetById.Replace("{id}", id.ToString()));

            var response = await responseMessage.Content.ReadFromJsonAsync<Response<VehicleDto?>>();

            return response!;
        }

        public async Task<Response<List<VehicleDto>>> GetAllAsync(FilterVehicleDto? filter = null)
        {
            var url = filter == null ? Router.Vehicles.GetAll : Router.Vehicles.GetAll + QueryStringHelpers.QueryFromObject(filter);
            var responseMessage = await _httpClient.GetAsync(url);

            var response = await responseMessage.Content.ReadFromJsonAsync<Response<List<VehicleDto>>>();

            return response!;
        }
    }
}
