using DigitalLibrary.Shared.DTO;
using Microsoft.JSInterop;
using System.Net.Http.Json;

public class AuthServices
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;

    public AuthServices(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public async Task<string?> RefreshTokenAsync()
    {
        var refreshToken = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "refreshToken");

        if (string.IsNullOrEmpty(refreshToken))
        {
            return null;
        }

        var request = new { refreshToken = refreshToken };
        var response = await _httpClient.PostAsJsonAsync("api/User/refresh-token", request);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<LoginReponseDTO>();

        if (result != null)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "accessToken", result.AccessToken);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "refreshToken", result.RefreshToken);

            return result.AccessToken;
        }
        else
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "accessToken");
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "refreshToken");
        }

        return null;
    }
}
