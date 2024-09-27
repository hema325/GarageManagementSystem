using GMS.Client.Services.Account;
using Microsoft.AspNetCore.Components;

namespace GMS.Client
{
    public partial class App
    {
        [Inject]
        public IAccountService AccountService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private bool IsRelogin = false;

        protected override async Task OnInitializedAsync()
        {
            IsRelogin = true;

            await AccountService.ReloginAsync();

            IsRelogin = false;
        }
    }
}
