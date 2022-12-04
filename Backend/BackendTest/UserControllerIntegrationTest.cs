using System.Net;
using System.Net.Http.Json;

namespace BackendTest;

[Collection("Sequential")]
public class UserControllerIntegrationTest : IClassFixture<TestingWebAppFactory>
{
    private readonly HttpClient _client;

    public UserControllerIntegrationTest(TestingWebAppFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task DeleteUser_WhileBeingLoggedInAsAdmin()
    {
        var token = await Helper.GetAdminAccessToken(_client);
        var response = await Helper.DeleteWithAuth(_client, "/api/User/deleteUser?userId=ID", token);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task DeleteUser_WhileBeingLoggedInNotAsTheTarget()
    {
        var token = await Helper.GetAccessToken(_client);
        var response = await Helper.DeleteWithAuth(_client, "/api/User/deleteUser?userId=ID", token);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteUser_WithoutBeingLoggedIn()
    {
        var response = await Helper.DeleteWithoutAuth(_client, "/api/User/deleteUser?userId=ID");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetUser_WhileBeingLoggedIn()
    {
        var token = await Helper.GetAccessToken(_client);
        var response = await Helper.GetWithAuth(_client, "/api/User/getUser", token);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetUser_WithoutBeingLoggedIn()
    {
        var response = await Helper.GetWithoutAuth(_client, "/api/User/getUser");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetUsers_AsAdmin()
    {
        var token = await Helper.GetAdminAccessToken(_client);
        var response = await Helper.GetWithAuth(_client, "/api/User/getUsers", token);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<List<object>>();
        Assert.NotNull(result);
        Assert.True(result.Count >= 2);
    }


    [Fact]
    public async Task GetUsers_AsNotAdmin()
    {
        var token = await Helper.GetAccessToken(_client);
        var response = await Helper.GetWithAuth(_client, "/api/User/getUsers", token);
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
}