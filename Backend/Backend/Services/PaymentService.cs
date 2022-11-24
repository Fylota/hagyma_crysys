using Backend.Dal;
using Backend.Exceptions;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class PaymentService : IPaymentService
{
    public PaymentService(AppDbContext context)
    {
        Context = context;
    }

    private AppDbContext Context { get; }

    public async Task<bool> BuyImageAsync(string imageId, string userId)
    {
        var image = await Context.Images.SingleOrDefaultAsync(i => i.Id == imageId);
        if (image == null) throw new ImageNotFoundException();
        var user = await Context.Users.Include(u => u.PurchasedImages).SingleOrDefaultAsync(u => u.Id == userId);
        if (user == null) throw new UserNotFoundException();
        if (user.PurchasedImages.Contains(image)) return true;

        user.PurchasedImages.Add(image);
        await Context.SaveChangesAsync();
        return true;
    }
}