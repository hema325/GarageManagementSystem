using Microsoft.JSInterop;

namespace GMS.Client.Services.Toastr
{
    public class ToastrService: IToastrService
    {
        private readonly IJSRuntime _jsRuntime;

        public ToastrService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SuccessAsync(string message, string? title = null)
        {
            await _jsRuntime.InvokeVoidAsync("toastr.success", message, title);
        }
        
        public async Task WarningAsync(string message, string? title = null)
        {
            await _jsRuntime.InvokeVoidAsync("toastr.warning", message, title);
        }
        
        public async Task InfoAsync(string message, string? title = null)
        {
            await _jsRuntime.InvokeVoidAsync("toastr.info", message, title);
        }
        
        public async Task ErrorAsync(string message, string? title = null)
        {
            await _jsRuntime.InvokeVoidAsync("toastr.error", message, title);
        }
    }
}
