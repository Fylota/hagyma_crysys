using Backend.Dal;
using Backend.Dal.Entities;
using Backend.Exceptions;
using Backend.Extensions;
using Backend.Models;
using Backend.Models.Auth;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class UserService : IUserService
{
    public UserService(AppDbContext context, UserManager<DbUserInfo> userManager)
    {
        Context = context;
        UserManager = userManager;
    }

    private AppDbContext Context { get; }
    private UserManager<DbUserInfo> UserManager { get; }

    public async Task<DbUserInfo> GetUserByEmailAsync(string email)
    {
        var user = await Context.Users.SingleOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new UserNotFoundException();
        return user;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await Context.Users.Select(u => u.ToModel()).ToListAsync();
    }

    public async Task<User> DeleteUserAsync(string userId)
    {
        var user = await Context.Users.SingleOrDefaultAsync(u => u.Id == userId);
        if (user == null) throw new UserNotFoundException();
        await UserManager.SetLockoutEnabledAsync(user, true);
        await UserManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
        return user.ToModel();
    }

    public async Task<User> GetUserByIdAsync(string userId)
    {
        var user = await Context.Users.SingleOrDefaultAsync(u => u.Id == userId);
        if (user == null) throw new UserNotFoundException();
        return user.ToModel();
    }

    public async Task<User> UpdateUserAsync(string userId, UserChangeRequest user)
    {
        var resultUser = await Context.Users.SingleOrDefaultAsync(u => u.Id == userId);
        if (resultUser == null) throw new UserNotFoundException();
        var changeResult = await UserManager.ChangePasswordAsync(resultUser, user.CurrentPassword, user.NewPassword);
        if (!changeResult.Succeeded)
            throw new PasswordChangeException(changeResult.Errors.FirstOrDefault()!.Description);
        return resultUser.ToModel();
    }
}