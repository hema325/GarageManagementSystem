using GMS.Client.Services.Owners;
using GMS.Shared.Dtos.Responses.Owners;
using Microsoft.AspNetCore.Components;

namespace GMS.Client.Pages.Owners
{
    public partial class Owners
    {
        [Inject]
        public IOwnersService OwnersService { get; set; }

        private List<OwnerDto> OwnersList = new();
        private bool IsFetching = false;

        protected override async Task OnInitializedAsync()
        {
            IsFetching = true;

            var response = await OwnersService.GetAllAsync();

            if (response.Succeeded)
                OwnersList = response.Data!;

            IsFetching = false;
        }

        private async Task DeleteAsync(int id)
        {
            var response = await OwnersService.DeleteAsync(id);

            if (response.Succeeded)
                OwnersList.RemoveAll(o => o.Id == id);
        }
    }
}
