using System.Net;

namespace BackendTest;

[Collection("Sequential")]
public class PaymentControllerIntegrationTest : IClassFixture<TestingWebAppFactory>
{
    private readonly HttpClient _client;

    public PaymentControllerIntegrationTest(TestingWebAppFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task BuyExistentImage()
    {
        var token = await Helper.GetAccessToken(_client);
        var response = await Helper.PostWithAuth(_client, "/api/Payment/purchase?imageId=ImageID", token, new object());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(await response.Content.ReadAsStringAsync() == "true");
    }


    [Fact]
    public async Task BuyNonExistentImage()
    {
        var token = await Helper.GetAccessToken(_client);
        var response =
            await Helper.PostWithAuth(_client, "/api/Payment/purchase?imageId=NonExistentId", token, new object());
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}