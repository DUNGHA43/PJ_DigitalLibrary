using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.DTO;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;

namespace DigitalLibrary.Client.Services
{
    public class DocumentAuthorServices
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        private readonly UserServices _userServices;

        public DocumentAuthorServices(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager, UserServices userServices)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
            _userServices = userServices;
        }

        public async Task<List<DocumentAuthorDTO>> GetDocumentAuthorsAsync(int documentId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/DocumentAuthors/getall?documentId={documentId}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new List<DocumentAuthorDTO>();
                }
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<List<DocumentAuthorDTO>>();
                return result ?? new List<DocumentAuthorDTO>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi gọi API: {e.Message}");
                return new List<DocumentAuthorDTO>();
            }
        }

        public async Task<bool> AddDocumentAuthorAsync(DocumentAuthorDTO documentAuthor)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "api/DocumentAuthors/add");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());
                request.Content = JsonContent.Create(documentAuthor);

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
                Console.WriteLine($"Lỗi khi gán tác giả: {e.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteDocumentAuthorAsync(DocumentAuthorDTO documentAuthor)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, "api/DocumentAuthors/delete");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());
                request.Content = JsonContent.Create(documentAuthor);

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
                Console.WriteLine($"Lỗi khi bỏ gán tác giả: {e.Message}");
                return false;
            }
        }
    }
}
