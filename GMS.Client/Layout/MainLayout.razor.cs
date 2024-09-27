using GMS.Client.Services.Account;
using Microsoft.AspNetCore.Components;

namespace GMS.Client.Layout
{
    public partial class MainLayout
    {
        [Inject]
        public IAccountService AccountService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public async Task LogoutAsync()
        {
            await AccountService.LogoutAsync();
            NavigationManager.NavigateTo("/login");
        }
    }
}
