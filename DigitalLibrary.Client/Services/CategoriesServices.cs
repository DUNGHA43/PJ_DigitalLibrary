using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.DTO;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata;

namespace DigitalLibrary.Client.Services
{
    public class CategoriesServices
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        private readonly UserServices _userServices;

        public CategoriesServices(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager, UserServices userServices)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
            _userServices = userServices;
        }

        public async Task<PaginationResponse<CategoriesDTO>> GetCategoriesAsync(int pageNumber = 1, int pageSize = 10, string searchName = "")
        {
            try
            {
                var request= new HttpRequestMessage(HttpMethod.Get, $"api/Categorse/getall?pageNumber={pageNumber}&pageSize={pageSize}&searchName={searchName}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());

                var response = await _httpClient.SendAsync(request);

                if(response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new PaginationResponse<CategoriesDTO> { Data = new List<CategoriesDTO>() };
                }
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<PaginationResponse<CategoriesDTO>>();
                return result ?? new PaginationResponse<CategoriesDTO>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi gọi API: {e.Message}");
                return new PaginationResponse<CategoriesDTO>();
            }
        }

        public async Task<List<CategoriesDTO>> GetCategoriesAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/Categorse/getall-noqueries");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new List<CategoriesDTO>();
                }
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<List<CategoriesDTO>>();
                return result ?? new List<CategoriesDTO>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi gọi API: {e.Message}");
                return new List<CategoriesDTO>();
            }
        }

        public async Task<bool> AddCategoryAsync(CategoriesDTO category)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "api/Categorse/addcategory");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());
                request.Content = JsonContent.Create(category);

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
                Console.WriteLine($"Lỗi khi thêm thể loại: {e.Message}");
                return false;
            }
        }

        public async Task<bool> EditCategoryAsync(CategoriesDTO category)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, "api/Categorse/editcategory");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());
                request.Content = JsonContent.Create(category);

                var response = await _httpClient.SendAsync(request);
                if(response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return false;
                }

                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi sửa thể loại: {e.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, "api/Categorse/deletecategory");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());
                request.Content= JsonContent.Create(id);

                var response = await _httpClient.SendAsync(request);
                if(response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return false;
                }
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi xóa thể loại: {e.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteCategoriesMultiAsync(List<int> selectedIds)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, "api/Categorse/deletemulti-category");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());
                request.Content = JsonContent.Create(selectedIds);

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
                Console.WriteLine($"Lỗi khi xóa thể loại: {e.Message}");
                return false;
            }
        }
    }
}
