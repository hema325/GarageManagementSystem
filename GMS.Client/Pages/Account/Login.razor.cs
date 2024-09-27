using GMS.Client.Authentication;
using GMS.Client.Services.Account;
using GMS.Client.Services.Toastr;
using GMS.Shared.Dtos.Requests.Account;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace GMS.Client.Pages.Account
{
    public partial class Login
    {
        [SupplyParameterFromQuery]
        public string? ReturnUrl { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IAccountService AccountService { get; set; }

        [Inject ]
        public IToastrService ToastrService { get; set; }

        [Inject]
        public IAuthenticationStateProvider _auth { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationState { get; set; }

        private LoginRequestDto LoginRequest = new();
        private bool IsLogging = false;

        public async Task OnValidSubmit()
        {
            IsLogging = true;

            var response = await AccountService.LoginAsync(LoginRequest);

            if (response.Succeeded)
            {
                if (!string.IsNullOrEmpty(ReturnUrl))
                    NavigationManager.NavigateTo(ReturnUrl);
                else
                    NavigationManager.NavigateTo("/");
            }

            IsLogging = false;
        }
    }
}
