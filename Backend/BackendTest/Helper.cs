using System.Net.Http.Json;
using Backend.Models.Auth;

namespace BackendTest;

internal static class Helper
{
    public static int Id = 0;
    public static readonly object Lock = new();

    public static async Task<HttpResponseMessage> DeleteWithAuth(HttpClient client, string url, string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        request.Headers.Add("Authorization", $"Bearer {token}");
        return await client.SendAsync(request);
    }

    public static async Task<HttpResponseMessage> DeleteWithoutAuth(HttpClient client, string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        return await client.SendAsync(request);
    }

    public static async Task<HttpResponseMessage> GetWithAuth(HttpClient client, string url, string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("Authorization", $"Bearer {token}");
        return await client.SendAsync(request);
    }

    public static async Task<HttpResponseMessage> GetWithoutAuth(HttpClient client, string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        return await client.SendAsync(request);
    }

    public static async Task<HttpResponseMessage> PostWithAuth<T>(HttpClient client, string url, string token, T body)
    {
        var jsonRequest = JsonContent.Create(body);
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = jsonRequest;
        request.Headers.Add("Authorization", $"Bearer {token}");
        return await client.SendAsync(request);
    }

    public static async Task<HttpResponseMessage> PostWithoutAuth<T>(HttpClient client, string url, T body)
    {
        var jsonRequest = JsonContent.Create(body);
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = jsonRequest
        };
        return await client.SendAsync(request);
    }

    public static async Task<HttpResponseMessage> PutWithAuth<T>(HttpClient client, string url, string token, T body)
    {
        var jsonRequest = JsonContent.Create(body);
        var request = new HttpRequestMessage(HttpMethod.Put, url);
        request.Content = jsonRequest;
        request.Headers.Add("Authorization", $"Bearer {token}");
        return await client.SendAsync(request);
    }

    public static async Task<string> GetAccessToken(HttpClient client)
    {
        var loginRequest = new LoginRequest {Email = "test@test.com", Password = "Test1!"};
        return await GetAccessToken(client, loginRequest);
    }
    public static async Task<string> GetAccessToken(HttpClient client, LoginRequest loginRequest)
    {
        var jsonRequest = JsonContent.Create(loginRequest);
        var response = await client.PostAsync("/auth/login", jsonRequest);
        response.EnsureSuccessStatusCode();
        var token = await response.Content.ReadAsStringAsync();
        return token.Replace("\"", "");
    }

    public static async Task<string> GetAdminAccessToken(HttpClient client)
    {
        var loginRequest = new LoginRequest {Email = "testadmin@testadmin.com", Password = "TestAdmin1!"};
        var jsonRequest = JsonContent.Create(loginRequest);
        var response = await client.PostAsync("/auth/login", jsonRequest);
        response.EnsureSuccessStatusCode();
        var token = await response.Content.ReadAsStringAsync();
        return token.Replace("\"", "");
    }
}