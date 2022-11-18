using Backend.Dal.Entities;
using Backend.Models;

namespace Backend.Services.Interfaces;

public interface IUserService
{
    public Task<List<User>> GetUsersAsync();
    public Task<User> DeleteUserAsync(string userId);
    public Task<DbUserInfo> GetUserByEmailAsync(string email);
    public Task<User> GetUserByIdAsync(string userId);
    public Task<User> UpdateUserAsync(string userId, User user);
}