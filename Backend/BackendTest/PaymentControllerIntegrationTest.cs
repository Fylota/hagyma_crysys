using System.Net;

namespace BackendTest
{
    [Collection("Sequential")]
    public class PaymentControllerIntegrationTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;

        public PaymentControllerIntegrationTest(TestingWebAppFactory<Program> factory) => _client = factory.CreateClient();


        [Fact]
        public async void BuyNonExistentImage()
        {
            var token = await AuthHelper.GetAccessToken(_client!);
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Payment/purchase?imageId=NonExistentId");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void BuyExistentImage()
        {
            var token = await AuthHelper.GetAccessToken(_client!);
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Payment/purchase?imageId=ImageID");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var response = await _client!.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(await response.Content.ReadAsStringAsync() == "true");
        }

    }
}