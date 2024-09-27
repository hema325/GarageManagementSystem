
namespace GMS.Client.Services.Toastr
{
    public interface IToastrService
    {
        Task ErrorAsync(string message, string? title = null);
        Task InfoAsync(string message, string? title = null);
        Task SuccessAsync(string message, string? title = null);
        Task WarningAsync(string message, string? title = null);
    }
}
