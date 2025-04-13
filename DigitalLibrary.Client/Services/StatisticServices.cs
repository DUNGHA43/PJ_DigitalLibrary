using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Http.Json;

namespace DigitalLibrary.Client.Services
{
    public class StatisticServices
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        private readonly UserServices _userServices;

        public StatisticServices(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager, UserServices userServices)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
            _userServices = userServices;
        }

        public async Task<List<StatisticDTO>> GetStatisticAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Statistics/GetStatistics_noauthozire");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<StatisticDTO>>() ?? new List<StatisticDTO>();
                }
                else
                {
                    return new List<StatisticDTO>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi gọi API: {ex.Message}");
                return new List<StatisticDTO>();
            }
        }

        public async Task<ViewAndDowloadStatisticResponse> GetViewAndDowloadStatisticAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "api/Statistics/getviewanddowloadstatistic");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new ViewAndDowloadStatisticResponse();
                }
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<ViewAndDowloadStatisticResponse>();
                return result ?? new ViewAndDowloadStatisticResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi gọi API: {ex.Message}");
                return new ViewAndDowloadStatisticResponse();
            }
        }

        public async Task<TrafficStatsDTO> GetStatsStatisticAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "api/Statistics/getstats");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new TrafficStatsDTO();
                }
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<TrafficStatsDTO>();
                return result ?? new TrafficStatsDTO();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi gọi API: {ex.Message}");
                return new TrafficStatsDTO();
            }
        }
    }
}
