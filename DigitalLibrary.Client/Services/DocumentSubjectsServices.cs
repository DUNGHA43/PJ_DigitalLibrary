using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.DTO;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;

namespace DigitalLibrary.Client.Services
{
    public class DocumentSubjectsServices
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        private readonly UserServices _userServices;

        public DocumentSubjectsServices(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager, UserServices userServices)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
            _userServices = userServices;
        }

        public async Task<List<DocumentSubjectDTO>> GetDocumentSubjectsAsync(int documentId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/DocumentSubjects/getall?documentId={documentId}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new List<DocumentSubjectDTO>();
                }
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<List<DocumentSubjectDTO>>();
                return result ?? new List<DocumentSubjectDTO>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi gọi API: {e.Message}");
                return new List<DocumentSubjectDTO>();
            }
        }

        public async Task<bool> AddDocumentSubjectAsync(DocumentSubjectDTO documentSubject)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "api/DocumentSubjects/add");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());
                request.Content = JsonContent.Create(documentSubject);

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
                Console.WriteLine($"Lỗi khi gán chủ đề: {e.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteSubjectCategoryAsync(DocumentSubjectDTO documentSubject)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, "api/DocumentSubjects/delete");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());
                request.Content = JsonContent.Create(documentSubject);

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
                Console.WriteLine($"Lỗi khi bỏ gán chủ đề: {e.Message}");
                return false;
            }
        }
    }
}
