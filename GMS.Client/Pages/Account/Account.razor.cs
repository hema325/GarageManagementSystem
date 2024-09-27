using GMS.Client.Services.Account;
using GMS.Shared.Dtos.Requests.Account;
using Microsoft.AspNetCore.Components;

namespace GMS.Client.Pages.Account
{
    public partial class Account
    {
        [Inject]
        public IAccountService AccountService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        private ChangePasswordDto ChangePasswordDto = new();
        private bool IsChanging = false;

        private async Task ChangePasswordAsync()
        {
            IsChanging = true;

            var response = await AccountService.ChangePasswordAsync(ChangePasswordDto);
            if (response.Succeeded)
            {
                await AccountService.LogoutAsync();
                NavigationManager.NavigateTo("/login");
            }

            IsChanging = false;
        }
    }
}
