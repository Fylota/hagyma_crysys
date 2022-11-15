using Backend.Dal;
using Backend.Extensions;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Duende.IdentityServer.Models.IdentityResources;

namespace Backend.Services;

public class UserService : IUserService
{
    private AppDbContext Context { get; }

    public UserService(AppDbContext context)
    {
        Context = context;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await Context.Users.Select(u => u.ToModel()).ToListAsync();
    }

    public async Task<User?> DeleteUserAsync(string userId)
    {
        var user = await Context.Users.SingleOrDefaultAsync(u => u.Id == userId);
        if (user == null) return null;
        //TODO Figure out deleting;
        return user.ToModel();
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await Context.Users.SingleOrDefaultAsync(u => u.Email == email);
        return user?.ToModel();
    }

    public async Task<User?> GetUserByIdAsync(string userId)
    {
        var user = await Context.Users.SingleOrDefaultAsync(u => u.Id == userId);
        return user?.ToModel();
    }

    public async Task<User?> UpdateUserAsync(string userId, User user)
    {
        var resultUser = await Context.Users.SingleOrDefaultAsync(u => u.Id == userId);
        if (resultUser == null) return null;
        //TODO Change Data
        await Context.SaveChangesAsync();
        return resultUser.ToModel();
    }
}