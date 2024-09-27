using GMS.Client.Services.Toastr;
using GMS.Shared.Dtos.Responses.Global;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace GMS.Client.Interceptors
{
    public class ErrorHandlerInterceptor: DelegatingHandler
    {
        private readonly IToastrService _toastr;
        private readonly NavigationManager _navigationManager;

        public ErrorHandlerInterceptor(IToastrService toastr, NavigationManager navigationManager)
        {
            _toastr = toastr;
            _navigationManager = navigationManager;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var responseMessage = await base.SendAsync(request, cancellationToken);

            if (responseMessage.StatusCode == HttpStatusCode.NotFound)
                _navigationManager.NavigateTo("/not-found");
            else if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
                _navigationManager.NavigateTo("/server-error");
            else if (responseMessage.StatusCode == HttpStatusCode.Forbidden)
                _navigationManager.NavigateTo("/access-denied");
            else
            {
                if (request.Method != HttpMethod.Get)
                {
                    var response = await responseMessage.Content
                        .ReadFromJsonWithStreamResetAsync<Response<object>>();

                    if (response != null && response.Message != null)
                    {
                        if (responseMessage.IsSuccessStatusCode)
                            await _toastr.SuccessAsync(response.Message);
                        else
                            await _toastr.ErrorAsync(response.Message);
                    }
                }
            }

            return responseMessage;
        }
    }
}
