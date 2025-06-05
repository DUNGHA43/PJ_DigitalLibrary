using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;

namespace DigitalLibrary.Client.Services
{
    public class TrafficLogServices
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        private readonly UserServices _userServices;

        public TrafficLogServices(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager, UserServices userServices)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
            _userServices = userServices;
        }

        public async Task<bool> LogTraffic()
        {
            try
            {
                var info = await _jsRuntime.InvokeAsync<BrowserInfoDTO>("getBrowserInfo");
                var ip = await _httpClient.GetStringAsync("https://Server.dunghalibrary.id.vn/api/TrafficLog/get-ip");

                var uri = new Uri(info.url);

                if (uri.AbsolutePath == "/")
                {
                    info.url = uri.GetLeftPart(UriPartial.Authority) + "/home";
                }

                var log = new TrafficLogDTO
                {
                    ipaddress = ip,
                    url = info.url,
                    useragent = info.useragent,
                };

                var request = new HttpRequestMessage(HttpMethod.Post, "api/TrafficLog/log");
                request.Content = JsonContent.Create(log);

                var response = await _httpClient.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return false;
                }
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi ghi log: {e.Message}");
                return false;
            }
        }
    }
}
