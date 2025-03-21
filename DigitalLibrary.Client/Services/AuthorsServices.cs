using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.DTO;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;

namespace DigitalLibrary.Client.Services
{
    public class AuthorsServices
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        private readonly UserServices _userServices;

        public AuthorsServices(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager, UserServices userServices)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
            _userServices = userServices;
        }

        public async Task<PaginationResponse<AuthorsDTO>> GetAuthorsAsync(int pageNumber = 1, int pageSize = 10, string searchName = "", string searchNation = "")
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/Author/getall?pageNumber={pageNumber}&pageSize={pageSize}&searchName={searchName}&searchNation={searchNation}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new PaginationResponse<AuthorsDTO> { Data = new List<AuthorsDTO>() };
                }
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<PaginationResponse<AuthorsDTO>>();
                return result ?? new PaginationResponse<AuthorsDTO>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi gọi API: {e.Message}");
                return new PaginationResponse<AuthorsDTO>();
            }
        }

        public async Task<List<AuthorsDTO>> GetAuthorsAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/Author/getall-noqueries");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new List<AuthorsDTO>();
                }
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<List<AuthorsDTO>>();
                return result ?? new List<AuthorsDTO>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi gọi API: {e.Message}");
                return new List<AuthorsDTO>();
            }
        }

        public async Task<bool> AddAuthorAsync(AuthorsDTO author)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "api/Author/addauthor");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());
                request.Content = JsonContent.Create(author);

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
                Console.WriteLine($"Lỗi khi thêm tác giả: {e.Message}");
                return false;
            }
        }

        public async Task<bool> EditAuthorAsync(AuthorsDTO author)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, "api/Author/editauthor");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());
                request.Content = JsonContent.Create(author);

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
                Console.WriteLine($"Lỗi khi sửa tác giả: {e.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, "api/Author/deleteauthor");
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
                Console.WriteLine($"Lỗi khi xóa tác giả: {e.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAuthorsMultiAsync(List<int> selectedIds)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, "api/Author/deletemulti-authors");
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
                Console.WriteLine($"Lỗi khi xóa tác giả: {e.Message}");
                return false;
            }
        }
    }
}
