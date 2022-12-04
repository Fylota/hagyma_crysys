using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Backend.Models;
using Microsoft.AspNetCore.Http;

namespace BackendTest;

[Collection("Sequential")]
public class CaffControllerIntegrationTest : IClassFixture<TestingWebAppFactory>
{
    private readonly HttpClient _client;

    public CaffControllerIntegrationTest(TestingWebAppFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetImage_WithoutBeingLoggedIn()
    {
        var response = await Helper.GetWithoutAuth(_client, "/api/Caff/getImage?imageId=ImageID");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetImage_WhileBeingLoggedIn()
    {
        var token = await Helper.GetAccessToken(_client);
        var response = await Helper.GetWithAuth(_client, "/api/Caff/getImage?imageId=ImageID",token);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetImage_NotExistent()
    {
        var token = await Helper.GetAccessToken(_client);
        var response = await Helper.GetWithAuth(_client, "/api/Caff/getImage?imageId=NotExistentId", token);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task AddComment_WithoutBeingLoggedIn()
    {
        var response = await Helper.PostWithoutAuth(_client, "/api/Caff/addComment?imageId=ImageID",new CommentRequest() {Content = "Content"});
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task AddComment_WhileBeingLoggedIn()
    {
        var token = await Helper.GetAccessToken(_client);
        var response = await Helper.PostWithAuth(_client, "/api/Caff/addComment?imageId=ImageID",token, new CommentRequest() { Content = "Content" });
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task AddComment_NotExistent()
    {
        var token = await Helper.GetAccessToken(_client);
        var response = await Helper.PostWithAuth(_client, "/api/Caff/addComment?imageId=NotExistentID", token, new CommentRequest() { Content = "Content" });
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task ListImages_WithoutBeingLoggedIn()
    {
        var response = await Helper.GetWithoutAuth(_client, "/api/Caff/listImages");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task ListImages_WhileBeingLoggedIn()
    {
        var token = await Helper.GetAccessToken(_client);
        var response = await Helper.GetWithAuth(_client, "/api/Caff/listImages",token);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PurchasedImages_WithoutBeingLoggedIn()
    {
        var response = await Helper.GetWithoutAuth(_client, "/api/Caff/purchasedImages");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task PurchasedImages_WhileBeingLoggedIn()
    {
        var token = await Helper.GetAccessToken(_client);
        var response = await Helper.GetWithAuth(_client, "/api/Caff/purchasedImages", token);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task UploadedImages_WithoutBeingLoggedIn()
    {
        var response = await Helper.GetWithoutAuth(_client, "/api/Caff/uploadedImages");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UploadedImages_WhileBeingLoggedIn()
    {
        var token = await Helper.GetAccessToken(_client);
        var response = await Helper.GetWithAuth(_client, "/api/Caff/uploadedImages", token);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task DeleteComment_WithoutBeingLoggedIn()
    {
        var response = await Helper.DeleteWithoutAuth(_client, "/api/Caff/deleteComment?commentId=CommentToDelete");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteComment_WhileBeingLoggedInNotAsAdmin()
    {
        var token = await Helper.GetAccessToken(_client);
        var response = await Helper.DeleteWithAuth(_client, "/api/Caff/deleteComment?commentId=CommentToDelete",token);
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task DeleteComment_WhileBeingLoggedInAsAdmin()
    {
        var token = await Helper.GetAdminAccessToken(_client);
        var response = await Helper.DeleteWithAuth(_client, "/api/Caff/deleteComment?commentId=CommentToDelete", token);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task DeleteImage_WithoutBeingLoggedIn()
    {
        var response = await Helper.DeleteWithoutAuth(_client, "/api/Caff/deleteImage?imageId=ImageToDelete");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteImage_WhileBeingLoggedIn()
    {
        var token = await Helper.GetAccessToken(_client);
        var response = await Helper.DeleteWithAuth(_client, "/api/Caff/deleteImage?imageId=ImageToDelete", token);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task DownloadImage_WithoutBeingLoggedIn()
    {
        var response = await Helper.GetWithoutAuth(_client, "/api/Caff/downloadImage?imageId=ImageID");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DownloadImage_NotPurchasedImage()
    {
        var token = await Helper.GetAccessToken(_client);
        var response = await Helper.GetWithAuth(_client, "/api/Caff/downloadImage?imageId=ImageToNotDownload", token);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DownloadImage_PurchasedImage()
    {
        var token = await Helper.GetAccessToken(_client);
        var response = await Helper.GetWithAuth(_client, "/api/Caff/downloadImage?imageId=ImageToDownload", token);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task UploadImage_WithoutBeingLoggedIn()
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/Caff/uploadImage");
        var content = new MultipartFormDataContent();
        request.Content = content;
        var response = await _client.SendAsync(request);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}