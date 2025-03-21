using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.DTO;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;

namespace DigitalLibrary.Client.Services
{
    public class DocumentCategoriesServices
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        private readonly UserServices _userServices;

        public DocumentCategoriesServices(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager, UserServices userServices)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
            _userServices = userServices;
        }

        public async Task<List<DocumentCategoriesDTO>> GetDocumentCategoriesAsync(int documentId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/DocumentCategories/getall?documentId={documentId}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new List<DocumentCategoriesDTO>();
                }
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<List<DocumentCategoriesDTO>>();
                return result ?? new List<DocumentCategoriesDTO>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi gọi API: {e.Message}");
                return new List<DocumentCategoriesDTO>();
            }
        }

        public async Task<bool> AddDocumentCategoryAsync(DocumentCategoriesDTO documentCategory)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "api/DocumentCategories/add");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());
                request.Content = JsonContent.Create(documentCategory);

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
                Console.WriteLine($"Lỗi khi gán thể loại: {e.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteDocumentCategoryAsync(DocumentCategoriesDTO documentCategories)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, "api/DocumentCategories/delete");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());
                request.Content = JsonContent.Create(documentCategories);

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
                Console.WriteLine($"Lỗi khi bỏ gán thể loại: {e.Message}");
                return false;
            }
        }
    }
}
