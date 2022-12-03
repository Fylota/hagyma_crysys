using System.Net;
using System.Net.Http.Json;
using Backend.Models.Auth;
using Microsoft.AspNetCore.Http;

namespace BackendTest
{
    public class AuthenticationControllerIntegrationTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;

        public AuthenticationControllerIntegrationTest(TestingWebAppFactory<Program> factory) => _client = factory.CreateClient();

        [Fact]
        public async void Login_WithNonExistentAccount()
        {
            var loginRequest = new LoginRequest() {Email = "test123@test.com", Password = "Password"};
            var jsonRequest = JsonContent.Create(loginRequest);
            var response = await _client!.PostAsync("/auth/login", jsonRequest);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void Login_WithExistentAccount()
        {
            var loginRequest = new LoginRequest() { Email = "test@test.com", Password = "Test1!" };
            var jsonRequest = JsonContent.Create(loginRequest);
            var response = await _client!.PostAsync("/auth/login", jsonRequest);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Register_WithWeakPassword()
        {
            var registerRequest = new RegisterRequest()
                {Email = "test1@gmail.com", Username = "test1", Password = "weak"};
            var jsonRequest = JsonContent.Create(registerRequest);
            var response = await _client!.PostAsync("/auth/register", jsonRequest);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void Register_WithGoodPassword()
        {
            var registerRequest = new RegisterRequest()
                { Email = "test1@gmail.com", Username = "test1", Password = "NotWeak1!" };
            var jsonRequest = JsonContent.Create(registerRequest);
            var response = await _client!.PostAsync("/auth/register", jsonRequest);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Logout_WithoutBeingLoggedIn()
        {
            var response = await _client!.GetAsync("/auth/logout");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void Logout_WhileBeingLoggedIn()
        {
            var token = await AuthHelper.GetAccessToken(_client!);
            var request = new HttpRequestMessage(HttpMethod.Get, "/auth/logout");
            request.Headers.Add("Authorization",$"Bearer {token}");
            var response = await _client!.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}