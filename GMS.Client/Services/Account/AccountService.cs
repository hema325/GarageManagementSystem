using Blazored.LocalStorage;
using FSharp.Data;
using GMS.Client.Authentication;
using GMS.Shared.Dtos.Requests.Account;
using GMS.Shared.Dtos.Responses.Account;
using GMS.Shared.Dtos.Responses.Global;
using System.Net.Http.Json;

namespace GMS.Client.Services.Account
{
    public class AccountService: IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly IAuthenticationStateProvider _authStateProvider;
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountService(HttpClient httpClient,
                              ILocalStorageService localStorageService,
                              IAuthenticationStateProvider authStateProvider,
                              IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _authStateProvider = authStateProvider;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Response<UserSessionDto>> LoginAsync(LoginRequestDto dto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync(Router.Account.Login, dto);

            var response = await responseMessage.Content
                    .ReadFromJsonAsync<Response<UserSessionDto>>();

            if (responseMessage.IsSuccessStatusCode)
            {
                await _localStorageService.SetItemEncryptedAsync(LocalStorageKeys.UserSession, response!.Data);
                _authStateProvider.NotifyAuthenticationStateChanged();
            }

            return response!;
        }

        public async Task<Response<object>> LogoutAsync()
        {
            var responseMessage = await _httpClient.PostAsync(Router.Account.RevokeRefreshToken, null);

            var response = await responseMessage.Content.ReadFromJsonAsync<Response<object>>();
            await _localStorageService.RemoveItemAsync(LocalStorageKeys.UserSession);
            _authStateProvider.NotifyAuthenticationStateChanged(); 

            return response!;
        }

        public async Task<Response<object>> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var responseMessage = await _httpClient.PutAsJsonAsync(Router.Account.ChangePassword, dto);

            return (await responseMessage.Content.ReadFromJsonAsync<Response<object>>())!;
        }

        public async Task<Response<UserSessionDto>> ReloginAsync()
        {
            var userSession = await _localStorageService.GetItemDecryptedAsync<UserSessionDto>(LocalStorageKeys.UserSession);
        
            if(userSession != null)
            {
                var dto = new RequestJwtTokenDto
                {
                    UserId = userSession.Id,
                    RefreshToken = userSession.RefreshToken
                };

                var client = _httpClientFactory.CreateClient(HttpClientKeys.APIWithoutInterceptors);
                var responseMessage = await client.PostAsJsonAsync(Router.Account.RequestJwtToken, dto);

                var response = await responseMessage.Content.ReadFromJsonAsync<Response<UserSessionDto>>();
                
                if(response!.Succeeded)
                    await _localStorageService.SetItemEncryptedAsync(LocalStorageKeys.UserSession, response!.Data);
                else
                    await _localStorageService.RemoveItemAsync(LocalStorageKeys.UserSession);

                _authStateProvider.NotifyAuthenticationStateChanged();

                return response;
            }

            return new Response<UserSessionDto>
            {
                Status = HttpStatusCodes.Unauthorized,
                Succeeded = false
            };
        }
    }
}
