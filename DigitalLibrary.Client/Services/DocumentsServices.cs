using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.DTO;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;
using System.Reflection.Metadata;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Components.Forms;

namespace DigitalLibrary.Client.Services
{
    public class DocumentsServices
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        private readonly UserServices _userServices;

        public DocumentsServices(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager, UserServices userServices)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
            _userServices = userServices;
        }

        public async Task<PaginationResponse<DocumentsDTO>> GetDocumentsAsync(int pageNumber = 1, int pageSize = 10, string searchName = "")
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/Document/getallinfodocuments?pageNumber={pageNumber}&pageSize={pageSize}&searchName={searchName}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new PaginationResponse<DocumentsDTO> { Data = new List<DocumentsDTO>() };
                }
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<PaginationResponse<DocumentsDTO>>();
                return result ?? new PaginationResponse<DocumentsDTO>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi gọi API: {e.Message}");
                return new PaginationResponse<DocumentsDTO>();
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

                var request = new HttpRequestMessage(HttpMethod.Get, $"api/Document/getimg/{id}");
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

        public async Task<string?> GetPdfBlobUrlAsync(int id)
        {
            try
            {
                var token = await _userServices.GetToken();
                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("⚠️ Không có token, không thể tải file.");
                    return null;
                }

                var request = new HttpRequestMessage(HttpMethod.Get, $"api/Document/getpdf/{id}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("⚠️ Token hết hạn hoặc không hợp lệ.");
                    return null;
                }

                response.EnsureSuccessStatusCode();

                var pdfBytes = await response.Content.ReadAsByteArrayAsync();
                var base64 = Convert.ToBase64String(pdfBytes);

                return $"data:application/pdf;base64,{base64}";
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi tải file PDF: {e.Message}");
                return null;
            }
        }

        public async Task<bool> AddDocumentAsync(DocumentsDTO document, IBrowserFile dcmFile, IBrowserFile imgFile)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "api/Document/adddocument");
                var token = await _userServices.GetToken();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var updatedBy = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "nameid")!.Value;

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(document.title!), "title");
                content.Add(new StringContent(document.publisher!), "publisher");
                content.Add(new StringContent(updatedBy.ToString()), "uploadedby");
                content.Add(new StringContent(document.description!), "description");
                content.Add(new StringContent(document.status.ToString().ToLower()), "status");

                if(dcmFile == null)
                {
                    return false;
                }
                var dcmStream = dcmFile.OpenReadStream(dcmFile.Size);
                var dcmContent = new StreamContent(dcmStream);
                dcmContent.Headers.ContentType = new MediaTypeHeaderValue(dcmFile.ContentType);
                content.Add(dcmContent, "dcmFile", dcmFile.Name);

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
                Console.WriteLine($"Lỗi khi upload file: {e.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteDocumentAsync(int id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, "api/Document/deletedocument");
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
    }
}
