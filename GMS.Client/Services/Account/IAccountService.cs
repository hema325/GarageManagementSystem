using GMS.Shared.Dtos.Requests.Account;
using GMS.Shared.Dtos.Responses.Account;
using GMS.Shared.Dtos.Responses.Global;

namespace GMS.Client.Services.Account
{
    public interface IAccountService
    {
        Task<Response<object>> ChangePasswordAsync(ChangePasswordDto dto);
        Task<Response<UserSessionDto>> LoginAsync(LoginRequestDto dto);
        Task<Response<object>> LogoutAsync();
        Task<Response<UserSessionDto>> ReloginAsync();
    }
}
