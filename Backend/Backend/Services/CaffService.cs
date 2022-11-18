using Backend.Dal;
using Backend.Exceptions;
using Backend.Extensions;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.IO;
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
        var user = Context.Entry(image).Collection(i => i.Buyers).Query().FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null && image.OwnerId != userId) throw new NotAllowedException();
        using var memStream = new MemoryStream(image.CaffFile);
        await using var dStream = new DeflateStream(memStream, CompressionMode.Decompress);
        var result = new MemoryStream();
        await dStream.CopyToAsync(result);
        return new Tuple<byte[], string>(result.ToArray(), image.CaffFileName);
    }

    public async Task<CaffDetails> UploadImage(string userId, CaffUploadRequest uploadRequest)
    {
        var image = uploadRequest.ToEntity();
        image.OwnerId = userId;
        image.CaffFileName = uploadRequest.CaffFile.FileName;
        image.UploadTime = DateTime.Now;
        using (var memStream = new MemoryStream())
        {
            await using (var dStream = new DeflateStream(memStream, CompressionLevel.Optimal))
            {
                await uploadRequest.CaffFile.OpenReadStream().CopyToAsync(dStream);
            }
            image.CaffFile = memStream.ToArray();
        }
        //TODO Replace these
        var directory = Directory.GetCurrentDirectory();
        var previewFile = Directory.GetFiles(directory, "*.jpg").FirstOrDefault();
        if (previewFile != null)
        {
            image.Preview = Convert.ToBase64String(await File.ReadAllBytesAsync(previewFile));
        }
        //END of TODO Replace these
        Context.Images.Add(image);
        await Context.SaveChangesAsync();
        return image.ToDetails();
    }
}