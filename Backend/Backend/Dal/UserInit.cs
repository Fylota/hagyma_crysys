using Backend.Dal.Entities;
using Backend.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace Backend.Dal;

public class UserInit : IHostedService
{
    public UserInit(IServiceScopeFactory serviceScopeFactory)
    {
        ServiceScopeFactory = serviceScopeFactory;
    }

    private IServiceScopeFactory ServiceScopeFactory { get; }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = ServiceScopeFactory.CreateScope();
        var userManager = scope.ServiceProvider.GetService<UserManager<DbUserInfo>>();
        await SeedData(userManager);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    private static async Task SeedData(UserManager<DbUserInfo>? userManager)
    {
        if (userManager == null) return;
        var admin = await userManager.FindByEmailAsync("admin@admin.com");
        if (admin == null)
            await userManager.CreateAsync(
                new DbUserInfo {Email = "admin@admin.com", UserName = "admin", Role = AuthRoles.Admin}, "Admin1!");
    }
}