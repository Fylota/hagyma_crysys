using System.Net;
using Backend.Models.Auth;

namespace BackendTest;

[Collection("Sequential")]
public class AuthenticationControllerIntegrationTest : IClassFixture<TestingWebAppFactory>
{
    private readonly HttpClient _client;

    public AuthenticationControllerIntegrationTest(TestingWebAppFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_WithExistentAccount()
    {
        var loginRequest = new LoginRequest {Email = "test@test.com", Password = "Test1!"};
        var response = await Helper.PostWithoutAuth(_client, "/auth/login", loginRequest);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Login_WithNonExistentAccount()
    {
        var loginRequest = new LoginRequest {Email = "test123@test.com", Password = "Password"};
        var response = await Helper.PostWithoutAuth(_client, "/auth/login", loginRequest);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Logout_WhileBeingLoggedIn()
    {
        var token = await Helper.GetAccessToken(_client);
        var response = await Helper.GetWithAuth(_client, "/auth/logout", token);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Logout_WithoutBeingLoggedIn()
    {
        var response = await Helper.GetWithoutAuth(_client, "/auth/logout");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Register_WithGoodPassword()
    {
        var registerRequest = new RegisterRequest
            {Email = "test1@gmail.com", Username = "test1", Password = "NotWeak1!"};
        var response = await Helper.PostWithoutAuth(_client, "/auth/register", registerRequest);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Register_WithWeakPassword()
    {
        var registerRequest = new RegisterRequest {Email = "test1@gmail.com", Username = "test1", Password = "weak"};
        var response = await Helper.PostWithoutAuth(_client, "/auth/register", registerRequest);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}