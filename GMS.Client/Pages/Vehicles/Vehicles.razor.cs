using GMS.Client.Services.Brands;
using GMS.Client.Services.Owners;
using GMS.Client.Services.Vehicles;
using GMS.Shared.Dtos.Requests.Vehicles;
using GMS.Shared.Dtos.Responses.Brands;
using GMS.Shared.Dtos.Responses.Owners;
using GMS.Shared.Dtos.Responses.Vehicles;
using Microsoft.AspNetCore.Components;
using static GMS.Shared.Constants.Router;

namespace GMS.Client.Pages.Vehicles
{
    public partial class Vehicles
    {
        [Inject]
        public IBrandsService BrandsService { get; set; }

        [Inject]
        public IOwnersService OwnersService { get; set; }

        [Inject]
        public IVehiclesService VehiclesService { get; set; }

        private List<BrandDto> BrandsList = new();
        private List<OwnerDto> OwnersList = new();
        private List<VehicleDto> VehiclesList = new();
        private FilterVehicleDto FilterVehicleDto = new();
        private bool IsFetching = false;
        private bool IsFiltering = false;

        protected override async Task OnInitializedAsync()
        {
            IsFetching = true;

            var brandsTask = LoadBrandsAsync();
            var ownersTask = LoadOwnersAsync();
            var vehiclesTask = LoadVehiclesAsync();

            await Task.WhenAll(brandsTask, ownersTask, vehiclesTask);

            IsFetching = false;
        }

        private async Task LoadVehiclesAsync()
        {
            var response = await VehiclesService.GetAllAsync(FilterVehicleDto);
            if (response.Succeeded)
                VehiclesList = response.Data!;
        }

        private async Task LoadOwnersAsync()
        {
            var ownersResponse = await OwnersService.GetAllAsync();
            if (ownersResponse.Succeeded)
                OwnersList = ownersResponse.Data!;
        }

        private async Task LoadBrandsAsync()
        {
            var brandsResponse = await BrandsService.GetAllAsync();
            if (brandsResponse.Succeeded)
                BrandsList = brandsResponse.Data!;
        }

        private async Task DeleteAsync(int id)
        {
            var response = await VehiclesService.DeleteAsync(id);

            if (response.Succeeded)
                VehiclesList.RemoveAll(v => v.Id == id);
        }

        private async Task ToggleStatusAsync(VehicleDto vehicle)
        {
            var newStatus = vehicle.Status == VehicleStatus.In.ToString() ? VehicleStatus.Out : VehicleStatus.In;
            var response = await VehiclesService.UpdateStatusAsync(vehicle.Id, newStatus);

            if (response.Succeeded)
                vehicle.Status = newStatus.ToString();
        }

        private async Task FilterAsync()
        {
            IsFiltering = true;
            await LoadVehiclesAsync();
            IsFiltering = false;
        }

        private async Task ResetAsync()
        { 
            FilterVehicleDto = new();
            await LoadVehiclesAsync();
        }
    }
}
