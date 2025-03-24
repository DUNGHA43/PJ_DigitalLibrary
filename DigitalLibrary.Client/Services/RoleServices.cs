using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace DigitalLibrary.Client.Services
{
    public class RoleServices
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        private readonly UserServices _userServices;

        public RoleServices(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager, UserServices userServices)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
            _userServices = userServices;
        }

        public async Task<List<RolesDTO>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Role/getallroles");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<RolesDTO>>() ?? new List<RolesDTO>();
                }
                else
                {
                    return new List<RolesDTO>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi gọi API vai trò: {ex.Message}");
                return new List<RolesDTO>();
            }
        }
    }
}
