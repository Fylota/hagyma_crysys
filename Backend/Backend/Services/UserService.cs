using Backend.Dal;
using Backend.Dal.Entities;
using Backend.Exceptions;
using Backend.Extensions;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class UserService : IUserService
{
    public UserService(AppDbContext context)
    {
        Context = context;
    }

    private AppDbContext Context { get; }

    public async Task<List<User>> GetUsersAsync()
    {
        return await Context.Users.Select(u => u.ToModel()).ToListAsync();
    }

    public async Task<User> DeleteUserAsync(string userId)
    {
        var user = await Context.Users.SingleOrDefaultAsync(u => u.Id == userId);
        if (user == null) throw new UserNotFoundException();
        //TODO Figure out deleting;
        return user.ToModel();
    }

    public async Task<DbUserInfo> GetUserByEmailAsync(string email)
    {
        var user = await Context.Users.SingleOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new UserNotFoundException();
        return user;
    }

    public async Task<User> GetUserByIdAsync(string userId)
    {
        var user = await Context.Users.SingleOrDefaultAsync(u => u.Id == userId);
        if (user == null) throw new UserNotFoundException();
        return user.ToModel();
    }

    public async Task<User> UpdateUserAsync(string userId, User user)
    {
        var resultUser = await Context.Users.SingleOrDefaultAsync(u => u.Id == userId);
        if (resultUser == null) throw new UserNotFoundException();
        //TODO Change Data
        await Context.SaveChangesAsync();
        return resultUser.ToModel();
    }
}