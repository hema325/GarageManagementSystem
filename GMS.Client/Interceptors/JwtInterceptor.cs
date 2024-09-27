
using Blazored.LocalStorage;
using GMS.Client.Services.Account;
using GMS.Shared.Dtos.Responses.Account;
using GMS.Shared.Dtos.Responses.Global;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Headers;

namespace GMS.Client.Interceptors
{
    public class JwtInterceptor: DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IServiceProvider _serviceProvider;
        private readonly NavigationManager _navigationManager;

        public JwtInterceptor(ILocalStorageService localStorage, IServiceProvider serviceProvider, NavigationManager navigationManager)
        {
            _localStorage = localStorage;
            _serviceProvider = serviceProvider;
            _navigationManager = navigationManager;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var userSession = await _localStorage.GetItemDecryptedAsync<UserSessionDto>(LocalStorageKeys.UserSession);

            if (userSession != null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userSession.JWTToken);

            var responseMessage = await base.SendAsync(request, cancellationToken);

            if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                var response = await responseMessage.Content.ReadFromJsonWithStreamResetAsync<Response<object>>();

                if (response!.Message == ErrorMessages.Unauthorized)
                {
                    var accountService = _serviceProvider.GetRequiredService<IAccountService>();
                    var reloginResponse = await accountService.ReloginAsync();

                    if (reloginResponse.Succeeded)
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", reloginResponse.Data!.JWTToken);
                        responseMessage = await base.SendAsync(request, cancellationToken);
                    }
                    else
                        _navigationManager.NavigateTo("/login");
                }
            }
            return responseMessage;
        }
    }
}
