using Backend.Dal.Entities;
using Backend.Models;
using Backend.Models.Auth;

namespace Backend.Services.Interfaces;

public interface IUserService
{
    public Task<List<User>> GetUsersAsync();
    public Task<User> DeleteUserAsync(string userId);
    public Task<DbUserInfo> GetUserByEmailAsync(string email);
    public Task<User> GetUserByIdAsync(string userId);
    public Task<User> UpdateUserAsync(string userId, UserChangeRequest user);
}