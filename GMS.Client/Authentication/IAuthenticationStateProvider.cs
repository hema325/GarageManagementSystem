using Microsoft.AspNetCore.Components.Authorization;

namespace GMS.Client.Authentication
{
    public interface IAuthenticationStateProvider
    {
        Task<AuthenticationState> GetAuthenticationStateAsync();
        void NotifyAuthenticationStateChanged();
    }
}
