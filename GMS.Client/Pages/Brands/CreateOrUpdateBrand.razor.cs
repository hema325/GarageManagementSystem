using GMS.Client.Models;
using GMS.Client.Services.Brands;
using GMS.Shared.Dtos.Requests.Brands;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace GMS.Client.Pages.Brands
{
    public partial class CreateOrUpdateBrand
    {
        [Parameter]
        public int? Id { get; set; }

        [Inject]
        public IBrandsService BrandsService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        private CreateOrUpdateBrandModel CreateOrUpdateBrandModel = new();
        private bool IsCreatingOrUpdating = false;

        protected override async Task OnInitializedAsync()
        {
            if(IsUpdate())
            {
                var response = await BrandsService.GetByIdAsync(Id!.Value);
                CreateOrUpdateBrandModel = new CreateOrUpdateBrandModel
                {
                    Name = response.Data!.Name
                };
            }
        }

        private async Task OnValidSubmit()
        {
            IsCreatingOrUpdating = true;

            if (IsUpdate())
            {
                var response = await BrandsService.UpdateAsync(Id!.Value, CreateOrUpdateBrandModel);
                if (response.Succeeded)
                    NavigationManager.NavigateTo("/brands");
            }
            else
            {
                var response = await BrandsService.CreateAsync(CreateOrUpdateBrandModel);
                if (response.Succeeded)
                    NavigationManager.NavigateTo("/brands");
            }

            IsCreatingOrUpdating = false;
        }

        private void OnInputFileChange(InputFileChangeEventArgs e)
            => CreateOrUpdateBrandModel.Image = e.File;

        private bool IsUpdate()
            => Id != null;
    }
}
