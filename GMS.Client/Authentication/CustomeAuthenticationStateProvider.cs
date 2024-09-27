using Blazored.LocalStorage;
using GMS.Shared.Dtos.Responses.Account;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace GMS.Client.Authentication
{
    public class CustomeAuthenticationStateProvider : AuthenticationStateProvider, IAuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        public CustomeAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userSession = await _localStorage.GetItemDecryptedAsync<UserSessionDto>(LocalStorageKeys.UserSession);

            if(userSession != null)
            {
                var identity = new ClaimsIdentity(ExtractSessionClaims(userSession), "JWT");
                var claimsPrincipal = new ClaimsPrincipal(identity);
                var state = new AuthenticationState(claimsPrincipal);

                return state;
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public void NotifyAuthenticationStateChanged()
            => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

        private static Claim[] ExtractSessionClaims(UserSessionDto userSession)
            => new[]
                        {
                new Claim(ClaimTypes.NameIdentifier, userSession.Id.ToString()),
                new Claim(ClaimTypes.Name, userSession.Name),
                new Claim(ClaimTypes.Email, userSession.Email),
                new Claim(ClaimTypes.Role, userSession.Role),
            };
    }
}
