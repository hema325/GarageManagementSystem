using GMS.Client.Services.Users;
using GMS.Shared.Dtos.Requests.Users;
using Microsoft.AspNetCore.Components;

namespace GMS.Client.Pages.Users
{
    public partial class Update
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IUsersService UsersService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private UpdateUserDto UpdateUserDto = new();
        private bool IsUpdating = false;

        protected override async Task OnInitializedAsync()
        {
            var response = await UsersService.GetByIdAsync(Id);

            if (response.Succeeded)
                UpdateUserDto = new UpdateUserDto 
                { 
                    Name = response.Data!.Name,
                    Email = response.Data!.Email,
                    Role = response.Data!.Role
                };
        }

        private async Task UpdateAsync()
        {
            IsUpdating = true;

            var response = await UsersService.UpdateAsync(Id, UpdateUserDto);

            if (response.Succeeded)
                NavigationManager.NavigateTo("/users");

            IsUpdating = false;
        }
    }
}
