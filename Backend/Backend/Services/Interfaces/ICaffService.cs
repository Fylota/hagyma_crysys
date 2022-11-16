using Backend.Models;

namespace Backend.Services.Interfaces;

public interface ICaffService
{
    public Task<List<CaffItem>> GetPurchasedImagesAsync(string userId);
    public Task<List<CaffItem>> GetUploadedImagesAsync(string userId);
    public Task<List<CaffItem>> GetImagesAsync();
    public Task<CaffDetails?> GetImageAsync(string imageId, string userId);
    public Task<CaffDetails?> DeleteImageAsync(string imageId, string userId, bool isAdmin);
}