using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace DigitalLibrary.Client.Services
{
    public class NationServices
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        private readonly UserServices _userServices;

        public NationServices(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager, UserServices userServices)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
            _userServices = userServices;
        }

        public async Task<List<NationsDTO>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Nation/getallnations");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<NationsDTO>>() ?? new List<NationsDTO>();
                }
                else
                {
                    return new List<NationsDTO>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi gọi API quốc tịch: {ex.Message}");
                return new List<NationsDTO>();
            }
        }
    }
}
