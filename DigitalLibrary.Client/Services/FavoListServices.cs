using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;

namespace DigitalLibrary.Client.Services
{
    public class FavoListServices
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        private readonly UserServices _userServices;

        public FavoListServices(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager, UserServices userServices)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
            _userServices = userServices;
        }
        public async Task<FavoListDTO> FindFavoListAsync(FavoListDTO favoListDTO)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/FavoritesList/getfavolist?documentId={favoListDTO.documentid}&userId={favoListDTO.userid}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return null;
                }
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<FavoListDTO>();
                return result ?? null;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi gọi API: {e.Message}");
                return null;
            }
        }

        public async Task<List<FavoListDTO>> GetFavoListByUser(int userId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/FavoritesList/getfavolistbyuser?userId={userId}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return null;
                }
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<List<FavoListDTO>>();
                return result ?? null;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi gọi API: {e.Message}");
                return null;
            }
        }

        public async Task<bool> AddFavoListAsync(FavoListDTO favoListDTO)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "api/FavoritesList/addfavolist");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());
                request.Content = JsonContent.Create(favoListDTO);

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
                Console.WriteLine($"Lỗi khi lưu tài liệu: {e.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteFavoListAsync(FavoListDTO favoListDTO)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, "api/FavoritesList/delete");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());
                request.Content = JsonContent.Create(favoListDTO);

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
                Console.WriteLine($"Lỗi khi lưu tài liệu: {e.Message}");
                return false;
            }
        }
    }
}
