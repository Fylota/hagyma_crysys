namespace Backend.Services.Interfaces;

public interface IPaymentService
{
    public Task<bool> BuyImageAsync(string imageId, string userId);
}