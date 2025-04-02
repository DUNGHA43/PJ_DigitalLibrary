using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Shared.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;

namespace DigitalLibrary.Client.Services
{
    public class UserManagementServices
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        private readonly UserServices _userServices;

        public UserManagementServices(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager, UserServices userServices)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
            _userServices = userServices;
        }

        public async Task<PaginationResponse<UsersDTO>> GetUsersAsync(int pageNumber = 1, int pageSize = 10, string searchName = "", string searchEmail = "", int? searchRole = null, bool? searchStatus = null)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/User/getall?pageNumber={pageNumber}&pageSize={pageSize}&searchName={searchName}&searchEmail={searchEmail}&searchRole={searchRole}&searchStatus={searchStatus}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new PaginationResponse<UsersDTO> { Data = new List<UsersDTO>() };
                }
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<PaginationResponse<UsersDTO>>();
                return result ?? new PaginationResponse<UsersDTO>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi gọi API: {e.Message}");
                return new PaginationResponse<UsersDTO>();
            }
        }

        public async Task<string?> GetImageAsync(int id)
        {
            try
            {
                var token = await _userServices.GetToken();
                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("⚠️ Không có token, không thể tải ảnh.");
                    return null;
                }

                var request = new HttpRequestMessage(HttpMethod.Get, $"api/User/getimg/{id}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"⚠️ API trả về lỗi: {response.StatusCode}");
                    return null;
                }

                var imageBytes = await response.Content.ReadAsByteArrayAsync();
                return $"data:image/jpeg;base64,{Convert.ToBase64String(imageBytes)}";
            }
            catch (Exception e)
            {
                Console.WriteLine($"❌ Lỗi khi tải ảnh: {e.Message}");
                return null;
            }
        }

        public async Task<bool> AddUserAsync(UsersDTO user, IBrowserFile imgFile)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "api/User/adduser");
                var token = await _userServices.GetToken();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(user.username!), "username");
                content.Add(new StringContent(user.password!), "password");
                content.Add(new StringContent(user.email!), "email");
                content.Add(new StringContent(user.fullname ?? ""), "fullname");
                content.Add(new StringContent(user.gender.ToString()!.ToLower() ?? "true"), "gender");
                content.Add(new StringContent(user.birthday.HasValue
                                                            ? user.birthday.Value.ToString("yyyy-MM-dd")
                                                            : DateTime.Now.ToString("yyyy-MM-dd")), "birthday");
                content.Add(new StringContent(user.roleid.ToString()!.Trim() ?? "7"), "roleid");
                content.Add(new StringContent(user.phonenumber ?? ""), "phonenumber");
                content.Add(new StringContent(user.identification ?? ""), "identification");
                content.Add(new StringContent(user.address ?? ""), "address");
                content.Add(new StringContent(user.status.ToString().ToLower()), "status");

                if (imgFile != null)
                {
                    var imgStream = imgFile.OpenReadStream(imgFile.Size);
                    var imgContent = new StreamContent(imgStream);
                    imgContent.Headers.ContentType = new MediaTypeHeaderValue(imgFile.ContentType);
                    content.Add(imgContent, "imgFile", imgFile.Name);
                }

                request.Content = content;

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
                Console.WriteLine($"Lỗi khi thêm người dùng: {e.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(UsersDTO user, IBrowserFile? imgFile)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, "api/User/edituser");
                var token = await _userServices.GetToken();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(user.username!), "username");
                content.Add(new StringContent(user.password!), "password");
                content.Add(new StringContent(user.email!), "email");
                content.Add(new StringContent(user.fullname!), "fullname");
                content.Add(new StringContent(user.gender.ToString()!.ToLower()), "gender");
                content.Add(new StringContent(user.birthday.ToString()!.ToLower()), "birthday");
                content.Add(new StringContent(user.roleid.ToString()!.Trim()), "roleid");
                content.Add(new StringContent(user.phonenumber!), "phonenumber");
                content.Add(new StringContent(user.identification!), "identification");
                content.Add(new StringContent(user.address!), "address");
                content.Add(new StringContent(user.status.ToString().ToLower()), "status");

                if (imgFile != null)
                {
                    var imgStream = imgFile.OpenReadStream(imgFile.Size);
                    var imgContent = new StreamContent(imgStream);
                    imgContent.Headers.ContentType = new MediaTypeHeaderValue(imgFile.ContentType);
                    content.Add(imgContent, "imgFile", imgFile.Name);
                }

                request.Content = content;

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
                Console.WriteLine($"Lỗi khi sửa người dùng: {e.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, "api/User/deleteuser");
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
                Console.WriteLine($"Lỗi khi xóa tài liệu: {e.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteUsersMultiAsync(List<int> selectedIds)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, "api/User/deletemulti-users");
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
                Console.WriteLine($"Lỗi khi xóa người dùng: {e.Message}");
                return false;
            }
        }
    }
}
