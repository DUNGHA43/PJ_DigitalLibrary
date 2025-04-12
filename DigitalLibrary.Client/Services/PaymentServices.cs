using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Net.payOS.Types;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Shared.DTO;
using System.Numerics;

namespace DigitalLibrary.Client.Services
{
    public class PaymentServices
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        private readonly UserServices _userServices;

        public PaymentServices(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager, UserServices userServices)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
            _userServices = userServices;
        }

        public async Task<Response> CreatePaymentLink(
            string productName,
            string description,
            int price,
            int userId,
            int planId)
        {
            try
            {
                // Tạo request
                var request = new CreatePaymentLinkRequest(
                    productName: productName,
                    description: description,
                    price: price,
                    returnUrl: $"https://localhost:7236/payment-success?userId={userId}&planId={planId}",
                    cancelUrl: "https://localhost:7236/payment-cancel"
                );

                var response = await _httpClient.PostAsJsonAsync("Order/create", request);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<Response>(responseString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    // Xử lý khi data tồn tại
                    if (result?.data != null)
                    {
                        // Cách 1: Dùng JsonElement để xử lý trực tiếp
                        if (result.data is JsonElement jsonElement)
                        {
                            var paymentResult = new CreatePaymentResult(
                                bin: jsonElement.GetProperty("bin").GetString(),
                                accountNumber: jsonElement.GetProperty("accountNumber").GetString(),
                                amount: jsonElement.GetProperty("amount").GetInt32(),
                                description: jsonElement.GetProperty("description").GetString(),
                                orderCode: (int)jsonElement.GetProperty("orderCode").GetInt64(),
                                currency: jsonElement.GetProperty("currency").GetString(),
                                paymentLinkId: jsonElement.GetProperty("paymentLinkId").GetString(),
                                status: jsonElement.GetProperty("status").GetString(),
                                expiredAt: jsonElement.GetProperty("expiredAt").ValueKind == JsonValueKind.Null ?
                                    null : jsonElement.GetProperty("expiredAt").GetInt64(),
                                checkoutUrl: jsonElement.GetProperty("checkoutUrl").GetString(),
                                qrCode: jsonElement.GetProperty("qrCode").GetString()
                            );

                            return new Response
                            {
                                error = result.error,
                                message = result.message,
                                data = paymentResult
                            };
                        }
                    }
                }

                return new Response
                {
                    error = -1,
                    message = "Không thể xử lý dữ liệu trả về",
                    data = null
                };
            }
            catch (HttpRequestException ex)
            {
                return new Response
                {
                    error = -1,
                    message = $"Lỗi kết nối: {ex.Message}",
                    data = null
                };
            }
            catch (JsonException ex)
            {
                return new Response
                {
                    error = -1,
                    message = $"Lỗi xử lý dữ liệu: {ex.Message}",
                    data = null
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    error = -1,
                    message = $"Lỗi hệ thống: {ex.Message}",
                    data = null
                };
            }
        }

        public async Task<List<PlanRevenueDTO>> GetPaymentHistoryAsync(int? day = null, int? month = null, int? year = null)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/PaymentHistory/getrevenue?day={day}&month={month}&year={year}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new List<PlanRevenueDTO>();
                }
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<List<PlanRevenueDTO>>();
                return result ?? new List<PlanRevenueDTO>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi gọi API: {e.Message}");
                return new List<PlanRevenueDTO>();
            }
        }

        public async Task<List<MonthlyPlanRevenueDTO>> GetMonthlyRevenueByYearAsync(int? year = null)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/PaymentHistory/getmonthlyrevenue?year={year}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new List<MonthlyPlanRevenueDTO>();
                }
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<List<MonthlyPlanRevenueDTO>>();
                return result ?? new List<MonthlyPlanRevenueDTO>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi gọi API: {e.Message}");
                return new List<MonthlyPlanRevenueDTO>();
            }
        }

        public async Task<bool> AddHistoryAndUpdatePlanAsync(int userId, int planId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"api/PaymentHistory/addhistoryandupdateplan?userId={userId}&planId={planId}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _userServices.GetToken());

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
