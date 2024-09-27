using GMS.Client.Services.Owners;
using GMS.Shared.Dtos.Requests.Owners;
using Microsoft.AspNetCore.Components;

namespace GMS.Client.Pages.Owners
{
    public partial class CreateOrUpdate
    {
        [Parameter]
        public int? Id { get; set; }

        [Inject]
        public IOwnersService OwnersService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private CreateOrUpdateOwnerDto CreateOrUpdateOwnerDto = new();
        private bool IsCreatingOrUpdating = false;

        protected override async Task OnInitializedAsync()
        {
            if (IsUpdate())
            {
                var response = await OwnersService.GetByIdAsync(Id!.Value);
                if (response.Succeeded)
                    CreateOrUpdateOwnerDto = new CreateOrUpdateOwnerDto
                    {
                        Name = response.Data!.Name,
                        Email = response.Data!.Email,
                        Phone = response.Data!.Phone
                    };
            }
        }

        public async Task OnValidSubmit() 
        {
            IsCreatingOrUpdating = true;

            if (IsUpdate())
            {
                var response = await OwnersService.UpdateAsync(Id!.Value, CreateOrUpdateOwnerDto);

                if (response.Succeeded)
                    NavigationManager.NavigateTo("/owners");
            }
            else
            {
                var response = await OwnersService.CreateAsync(CreateOrUpdateOwnerDto);

                if (response.Succeeded)
                    NavigationManager.NavigateTo("/owners");
            }

            IsCreatingOrUpdating = false;
        }

        private bool IsUpdate()
            => Id != null;
    }
}
