using GMS.Client.Services.Brands;
using GMS.Shared.Dtos.Responses.Brands;
using Microsoft.AspNetCore.Components;

namespace GMS.Client.Pages.Brands
{
    public partial class Brands
    {
        [Inject]
        public IBrandsService BrandsService { get; set; }


        private List<BrandDto> BrandsList = new();
        private bool IsFetching = false;

        protected override async Task OnInitializedAsync()
        {
            IsFetching = true;

            var response = await BrandsService.GetAllAsync();

            if (response.Succeeded)
                BrandsList = response.Data!;

            IsFetching = false;
        }

        private async Task DeleteAsync(int id)
        {
            var response = await BrandsService.DeleteAsync(id);

            if (response.Succeeded)
                BrandsList.RemoveAll(b => b.Id == id);
        }
    }
}
