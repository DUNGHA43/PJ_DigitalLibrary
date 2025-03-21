using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.DTO;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;

namespace DigitalLibrary.Client.Services
{
    public class SubjectsServices
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        private readonly UserServices _userServices;

        public SubjectsServices(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager, UserServices userServices)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
            _userServices = userServices;
        }

        public async Task<PaginationResponse<SubjectDTO>> GetSubjectsAsync(int pageNumber = 1, int pageSize = 10, string searchName = "")
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/Subject/getall?pageNumber={pageNumber}&pageSize={pageSize}&searchName={searchName}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new PaginationResponse<SubjectDTO> { Data = new List<SubjectDTO>() };
                }
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<PaginationResponse<SubjectDTO>>();
                return result ?? new PaginationResponse<SubjectDTO>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi gọi API: {e.Message}");
                return new PaginationResponse<SubjectDTO>();
            }
        }

        public async Task<List<SubjectDTO>> GetSubjectsAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/Subject/getall-noqueries");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new List<SubjectDTO>();
                }
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<List<SubjectDTO>>();
                return result ?? new List<SubjectDTO>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi gọi API: {e.Message}");
                return new List<SubjectDTO>();
            }
        }


        public async Task<bool> AddSubjectAsync(SubjectDTO subject)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "api/Subject/addsubject");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());
                request.Content = JsonContent.Create(subject);

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
                Console.WriteLine($"Lỗi khi thêm chủ đề: {e.Message}");
                return false;
            }
        }

        public async Task<bool> EditSubjectAsync(SubjectDTO subject)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, "api/Subject/editsubject");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());
                request.Content = JsonContent.Create(subject);

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
                Console.WriteLine($"Lỗi khi sửa chủ đề: {e.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteSubject(int id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, "api/Subject/deletesubject");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());
                request.Content = JsonContent.Create(id);

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
                Console.WriteLine($"Lỗi khi xóa chủ đề: {e.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteSubjectsMultiAsync(List<int> selectedIds)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, "api/Subject/deletemulti-subjects");
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
                Console.WriteLine($"Lỗi khi xóa chủ đề: {e.Message}");
                return false;
            }
        }
    }
}
