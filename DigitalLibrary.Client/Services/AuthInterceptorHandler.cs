using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace DigitalLibrary.Client.Services
{
    public class AuthInterceptorHandler : DelegatingHandler
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly UserServices _userServices;

        public AuthInterceptorHandler(IJSRuntime jsRuntime, UserServices userServices)
        {
            _jsRuntime = jsRuntime;
            _userServices = userServices;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "accessToken");

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var newToken = await _userServices.RefreshTokenAsync();

                if (!string.IsNullOrEmpty(newToken))
                {
                    // Tạo request mới với token mới (không sửa request cũ)
                    var newRequest = CloneHttpRequestMessage(request);
                    newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);

                    response.Dispose(); // Giải phóng response cũ tránh leak tài nguyên
                    return await base.SendAsync(newRequest, cancellationToken);
                }
            }

            return response;
        }

        private HttpRequestMessage CloneHttpRequestMessage(HttpRequestMessage request)
        {
            var newRequest = new HttpRequestMessage(request.Method, request.RequestUri);

            // Copy headers
            foreach (var header in request.Headers)
            {
                newRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            // Copy nội dung nếu có
            if (request.Content != null)
            {
                newRequest.Content = new StreamContent(request.Content.ReadAsStream());
                foreach (var header in request.Content.Headers)
                {
                    newRequest.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            return newRequest;
        }
    }
}
