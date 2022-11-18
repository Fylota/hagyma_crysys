using Backend.Dal;
using Backend.Exceptions;
using Backend.Extensions;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

namespace Backend.Services;

public class CaffService : ICaffService
{
    public CaffService(AppDbContext context)
    {
        Context = context;
    }

    private AppDbContext Context { get; }

    public async Task<CaffDetails?> DeleteImageAsync(string imageId, string userId, bool isAdmin)
    {
        var image = await Context.Images.SingleOrDefaultAsync(i => i.Id == imageId);
        if (image == null) return null;
        if (!isAdmin && image.OwnerId != userId) return null;
        image.IsDeleted = true;
        await Context.SaveChangesAsync();
        return image.ToDetails();
    }

    public async Task<CaffDetails?> GetImageAsync(string imageId, string userId)
    {
        var image = await Context.Images.Include(i => i.Comments).IgnoreQueryFilters()
            .SingleOrDefaultAsync(i => i.Id == imageId);
        if (image == null) return null;
        if (!image.IsDeleted) return image.ToDetails();
        var user = await Context.Users.Include(u => u.PurchasedImages).SingleOrDefaultAsync(u => u.Id == userId);
        if (user == null) return null;
        return user.PurchasedImages.Any(pi => pi.Id == imageId) ? image.ToDetails() : null;
    }

    public async Task<List<CaffItem>> GetImagesAsync()
    {
        return await Context.Images.Select(i => i.ToItem()).ToListAsync();
    }

    public async Task<List<CaffItem>> GetPurchasedImagesAsync(string userId)
    {
        var user = await Context.Users.Include(u => u.PurchasedImages).IgnoreQueryFilters()
            .SingleOrDefaultAsync(u => u.Id == userId);

        return user?.PurchasedImages.Select(u => u.ToItem()).ToList() ?? new List<CaffItem>();
    }

    public async Task<List<CaffItem>> GetUploadedImagesAsync(string userId)
    {
        var images = await Context.Images.Where(i => i.OwnerId == userId).ToListAsync();

        return images.Select(i => i.ToItem()).ToList();
    }

    public async Task<Tuple<byte[], string>> DownloadImageAsync(string imageId, string userId)
    {
        var image = await Context.Images.SingleOrDefaultAsync(i => i.Id == imageId);
        if (image == null) throw new ImageNotFoundException();
        var user = await Context.Users.SingleOrDefaultAsync(u => u.Id == userId);
        if (user == null) throw new UserNotFoundException();
        if (!user.PurchasedImages.Contains(image) && image.OwnerId != userId) throw new NotAllowedException();
        using var memStream = new MemoryStream(image.CaffFile);
        await using var dStream = new DeflateStream(memStream, CompressionMode.Decompress);
        var result = new MemoryStream();
        await dStream.CopyToAsync(result);
        return new Tuple<byte[], string>(result.ToArray(), image.CaffFileName);
    }
}