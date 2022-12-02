using System.Diagnostics.CodeAnalysis;
using System.Drawing.Imaging;
using System.IO.Compression;
using Backend.CAFFParser;
using Backend.Dal;
using Backend.Exceptions;
using Backend.Extensions;
using Backend.Helpers;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using DateTime = System.DateTime;

namespace Backend.Services;

public class CaffService : ICaffService
{
    public CaffService(AppDbContext context, ILogger<CaffService> logger)
    {
        Context = context;
        Logger = logger;
    }

    private AppDbContext Context { get; }
    private ILogger<CaffService> Logger { get; }

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
        var image = await Context.Images.Include(i => i.Comments).ThenInclude(c => c.User).IgnoreQueryFilters()
            .SingleOrDefaultAsync(i => i.Id == imageId);
        if (image == null) return null;
        var user = await Context.Users.SingleOrDefaultAsync(u => u.Id == userId);
        if (user == null) return null;
        var pImage = await Context.Entry(user).Collection(u => u.PurchasedImages).Query().Select(i => i.Id)
            .SingleOrDefaultAsync(id => id == imageId);
        var result = image.ToDetails();
        result.CanDownload = image.OwnerId == userId || pImage != null;
        if (!image.IsDeleted) return result;
        return user.PurchasedImages.Any(pi => pi.Id == imageId) ? result : null;
    }

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    public async Task<CaffDetails?> UploadImage(string userId, CaffUploadRequest uploadRequest)
    {
        var image = uploadRequest.ToEntity();
        image.OwnerId = userId;
        image.CaffFileName = uploadRequest.CaffFile.FileName;
        image.UploadTime = DateTime.Now;
        await using (var stream = uploadRequest.CaffFile.OpenReadStream())
        {
            using var memStream = new MemoryStream();
            await stream.CopyToAsync(memStream);
            try
            {
                var caff = CAFF.parseCAFF(new BytesVector(memStream.ToArray()));
                if (caff == null || !caff.isValid()) throw new InvalidCaffException();
                var preview = caff.generatePpmPreview();
                var bytes = new byte[preview.Count];
                preview.CopyTo(bytes);
                using var pixelStream = new MemoryStream(bytes);
                var bitmap = new PixelMap(pixelStream);
                using (var fileStream = new MemoryStream())
                {
                    bitmap.BitMap.Save(fileStream, ImageFormat.Jpeg);
                    image.Preview = Convert.ToBase64String(fileStream.ToArray());
                }

                var small = PixelMap.ResizeImage(bitmap.BitMap, 200);
                using (var fileStream = new MemoryStream())
                {
                    small.Save(fileStream, ImageFormat.Jpeg);
                    image.SmallPreview = Convert.ToBase64String(fileStream.ToArray());
                }
            }
            catch (InvalidCaffException)
            {
                throw;
            }
            catch (Exception e)
            {
                Logger.LogError("{}", e.Message);
                return null;
            }

            using var compressStream = new MemoryStream();
            await using (var dStream = new DeflateStream(compressStream, CompressionLevel.Optimal))
            {
                await memStream.CopyToAsync(dStream);
            }

            image.CaffFile = compressStream.ToArray();
        }

        Context.Images.Add(image);
        await Context.SaveChangesAsync();
        return image.ToDetails();
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
}