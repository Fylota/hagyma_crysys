using Backend.Dal;
using Backend.Dal.Entities;
using Backend.Models;
using Backend.Models.Auth;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BackendTest
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        private static readonly object Lock = new();
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<AppDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);
                services.AddDbContext<AppDbContext>(option =>
                {
                    option.UseInMemoryDatabase("TestDB");
                });

                //lock (Lock)
                {
                    var sp = services.BuildServiceProvider();
                    using (var scope = sp.CreateScope())
                    using (var db = scope.ServiceProvider.GetRequiredService<AppDbContext>())
                    using (var userManager = scope.ServiceProvider.GetRequiredService<UserManager<DbUserInfo>>())
                    {
                        db.Database.EnsureDeleted();
                        db.Database.EnsureCreated();
                        var testUser = userManager.FindByEmailAsync("test@test.com")?.Result;
                        var adminUser = userManager.FindByEmailAsync("testadmin@testadmin.com")?.Result;
                        if (testUser == null && adminUser == null)
                        {

                            var user1 = userManager.CreateAsync(new DbUserInfo() { Email = "test@test.com", UserName = "testuser" },
                                "Test1!").Result;
                            var user2 = userManager
                                .CreateAsync(
                                    new DbUserInfo()
                                    { Email = "testadmin@testadmin.com", UserName = "testadmin", Role = AuthRoles.Admin },
                                    "TestAdmin1!").Result;
                        }
                    }
                }
            });
        }
    }
}
