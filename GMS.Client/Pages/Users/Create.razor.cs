using GMS.Client.Services.Users;
using GMS.Shared.Dtos.Requests.Users;
using Microsoft.AspNetCore.Components;

namespace GMS.Client.Pages.Users
{
    public partial class Create
    {
        [Inject]
        public IUsersService UsersService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        private CreateUserDto CreateUserDto = new();
        private bool IsCreating = false;

        private async Task CreateAsync()
        {
            IsCreating = true;

            var response = await UsersService.CreateAsync(CreateUserDto);

            if (response.Succeeded)
                NavigationManager.NavigateTo("/users");

            IsCreating = false;
        }
    }
}
