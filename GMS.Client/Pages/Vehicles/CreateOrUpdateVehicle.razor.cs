using GMS.Client.Services.Brands;
using GMS.Client.Services.Owners;
using GMS.Client.Services.Vehicles;
using GMS.Shared.Dtos.Requests.Vehicles;
using GMS.Shared.Dtos.Responses.Brands;
using GMS.Shared.Dtos.Responses.Owners;
using Microsoft.AspNetCore.Components;

namespace GMS.Client.Pages.Vehicles
{
    public partial class CreateOrUpdateVehicle
    {
        [Parameter]
        public int? Id { get; set; }

        [Inject]
        public IBrandsService BrandsService { get; set; }

        [Inject]
        public IOwnersService OwnersService { get; set; }

        [Inject]
        public IVehiclesService VehiclesService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        private List<BrandDto> BrandsList = new();
        private List<OwnerDto> OwnersList = new();
        private CreateOrUpdateVehicleDto CreateOrUpdateVehicleDto = new();
        private bool IsCreatingOrUpdating = false;

        protected override async Task OnInitializedAsync()
        {
            var brandsTask = LoadBrandsAsync();
            var ownersTask = LoadOwnersAsync();
        
            await Task.WhenAll(brandsTask, ownersTask);

            if (IsUpdate())
                await LoadVehicleAsync();        
        }

        private async Task LoadVehicleAsync()
        {
            var vehicleResponse = await VehiclesService.GetByIdAsync(Id!.Value);
            if (vehicleResponse.Succeeded)
                CreateOrUpdateVehicleDto = new CreateOrUpdateVehicleDto
                {
                    LicensePlate = vehicleResponse.Data!.LicensePlate,
                    Description = vehicleResponse.Data!.Description,
                    Status = vehicleResponse.Data!.Status,
                    OwnerId = vehicleResponse.Data!.Owner.Id,
                    BrandId = vehicleResponse.Data!.Brand.Id
                };
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

        private async Task OnValidSubmit()
        {
            IsCreatingOrUpdating = true;

            if (IsUpdate())
            {
                var response = await VehiclesService.UpdateAsync(Id!.Value, CreateOrUpdateVehicleDto);
                if (response.Succeeded)
                    NavigationManager.NavigateTo("/vehicles");
            }
            else
            {
                var response = await VehiclesService.CreateAsync(CreateOrUpdateVehicleDto);
                if (response.Succeeded)
                    NavigationManager.NavigateTo("/vehicles");
            }

            IsCreatingOrUpdating = false;
        }

        private bool IsUpdate()
            => Id != null;
    }
}
