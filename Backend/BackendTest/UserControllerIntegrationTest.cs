using System.Net;
using System.Net.Http.Json;
using Backend.Models;

namespace BackendTest
{
    public class UserControllerIntegrationTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;

        public UserControllerIntegrationTest(TestingWebAppFactory<Program> factory) => _client = factory.CreateClient();


        [Fact]
        public async void GetUsers_AsNotAdmin()
        {
            var token = await AuthHelper.GetAccessToken(_client!);
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/User/getUsers");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async void GetUsers_AsAdmin()
        {
            var token = await AuthHelper.GetAdminAccessToken(_client!);
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/User/getUsers");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            List<object>? result = await response.Content.ReadFromJsonAsync<List<object>>();
            Assert.NotNull(result);
            Assert.True(result.Count >= 2);
        }

        [Fact]
        public async void GetUser_WhileBeingLoggedIn()
        {
            var token = await AuthHelper.GetAccessToken(_client!);
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/User/getUser");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetUser_WithoutBeingLoggedIn()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/User/getUser");
            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void DeleteUser_WithoutBeingLoggedIn()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/User/deleteUser?userId=ID");
            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void DeleteUser_WhileBeingLoggedInNotAsTheTarget()
        {
            var token = await AuthHelper.GetAccessToken(_client!);
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/User/deleteUser?userId=ID");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

    }
}