using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;

namespace DigitalLibrary.Client.Services
{
    public class UserServices
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        public UserServices(HttpClient httpClient, IJSRuntime jSRuntime, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _jsRuntime = jSRuntime;
            _navigationManager = navigationManager;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var loginRequest = new { Username = username, Password = password };

            var response = await _httpClient.PostAsJsonAsync("api/User/login", loginRequest);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var result = await response.Content.ReadFromJsonAsync<LoginReponseDTO>();

            if (result != null)
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "accessToken", result.AccessToken);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "refreshToken", result.RefreshToken);

                return true;
            }

            return false;
        }

        public async Task<string> RefreshTokenAsync()
        {
            var refeshToken = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "refeshToken");

            if (string.IsNullOrEmpty(refeshToken))
            {
                return string.Empty;
            }

            var request = new { refeshToken = refeshToken };
            var response = await _httpClient.PostAsJsonAsync("api/User/refresh-token", request);

            if (!response.IsSuccessStatusCode)
            {
                return string.Empty;
            }

            var result = await response.Content.ReadFromJsonAsync<LoginReponseDTO>();

            if (result != null)
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "accessToken", result.AccessToken);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "refreshToken", result.RefreshToken);

                return result.AccessToken;
            }

            return string.Empty;
        }

        public async Task<bool> IsLoggedIn()
        {
            var token = await GetToken();
            return !string.IsNullOrEmpty(token);
        }

        public async Task<string?> GetToken()
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "accessToken");
            return token;
        }

        public async Task<string> GetUserRole()
        {
            var token = await GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return string.Empty;
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role");

            return roleClaim?.Value!;
        }

        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "accessToken");
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "refreshToken");

            _navigationManager.NavigateTo("/", forceLoad: true);
        }
    }
}
