using Backend.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BackendTest
{
    internal static class AuthHelper
    {
        public static async Task<string> GetAccessToken(HttpClient client)
        {
            var loginRequest = new LoginRequest() { Email = "test@test.com", Password = "Test1!" };
            var jsonRequest = JsonContent.Create(loginRequest);
            var response = await client.PostAsync("/auth/login", jsonRequest);
            response.EnsureSuccessStatusCode();
            var token = await response.Content.ReadAsStringAsync();
            ;
            return token.Replace("\"", "");
        }

        public static async Task<string> GetAdminAccessToken(HttpClient client)
        {
            var loginRequest = new LoginRequest() { Email = "testadmin@testadmin.com", Password = "TestAdmin1!" };
            var jsonRequest = JsonContent.Create(loginRequest);
            var response = await client.PostAsync("/auth/login", jsonRequest);
            response.EnsureSuccessStatusCode();
            var token = await response.Content.ReadAsStringAsync();
            ;
            return token.Replace("\"", "");
        }
    }
}
