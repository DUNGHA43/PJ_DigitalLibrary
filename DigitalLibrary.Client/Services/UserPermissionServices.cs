using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;

namespace DigitalLibrary.Client.Services
{
    public class UserPermissionServices
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        private readonly UserServices _userServices;

        public UserPermissionServices(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager, UserServices userServices)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
            _userServices = userServices;
        }

        public async Task<List<UserPermissionsDTO>> GetUserPermissionAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/UserPermission/getall-noqueries");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new List<UserPermissionsDTO>();
                }
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<List<UserPermissionsDTO>>();
                return result ?? new List<UserPermissionsDTO>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi gọi API: {e.Message}");
                return new List<UserPermissionsDTO>();
            }
        }

        public async Task<bool> AddUserPermissionsAsync(UserPermissionsDTO userPermissions)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "api/UserPermission/adduserpermission");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());
                request.Content = JsonContent.Create(userPermissions);

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
                Console.WriteLine($"Lỗi khi thêm : {e.Message}");
                return false;
            }
        }

        public async Task<bool> EditUserPermissionsAsync(UserPermissionsDTO userPermissions)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, "api/UserPermission/updateuserpermission");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());
                request.Content = JsonContent.Create(userPermissions);

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
                Console.WriteLine($"Lỗi khi sửa quyền người dùng: {e.Message}");
                return false;
            }
        }
    }
}
