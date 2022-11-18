using Backend.Models;

namespace Backend.Services.Interfaces;

public interface ICaffService
{
    public Task<CaffDetails?> DeleteImageAsync(string imageId, string userId, bool isAdmin);
    public Task<CaffDetails?> GetImageAsync(string imageId, string userId);
    public Task<List<CaffItem>> GetImagesAsync();
    public Task<List<CaffItem>> GetPurchasedImagesAsync(string userId);
    public Task<List<CaffItem>> GetUploadedImagesAsync(string userId);
    public Task<Tuple<byte[], string>> DownloadImageAsync(string imageId, string userId);
    public Task<CaffDetails> UploadImage(string userId, CaffUploadRequest uploadRequest);
}