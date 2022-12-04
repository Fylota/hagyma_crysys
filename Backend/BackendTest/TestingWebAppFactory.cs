using Backend.Dal;
using Backend.Dal.Entities;
using Backend.Models.Auth;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BackendTest;

public class TestingWebAppFactory : WebApplicationFactory<Program>
{

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
                option.UseInMemoryDatabase("TestDB"); //+ Interlocked.Increment(ref Helper.Id));
            });

            lock (Helper.Lock)
            {
                var sp = services.BuildServiceProvider();
                // ReSharper disable once ConvertToUsingDeclaration
                using (var scope = sp.CreateScope())
                using (var db = scope.ServiceProvider.GetRequiredService<AppDbContext>())
                {
                    var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<DbUserInfo>>();
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                    var newTestUser = new DbUserInfo { Email = "test@test.com", UserName = "testUser" };
                    var hash = passwordHasher.HashPassword(newTestUser, "Test1!");
                    newTestUser.PasswordHash = hash;
                    db.Users.Add(newTestUser);
                    var newAdminUser = new DbUserInfo
                    { Email = "testadmin@testadmin.com", UserName = "testadmin", Role = AuthRoles.Admin };
                    hash = passwordHasher.HashPassword(newTestUser, "TestAdmin1!");
                    newAdminUser.PasswordHash = hash;
                    db.Users.Add(newAdminUser);
                    db.SaveChanges();
                    var image = db.Images.Include(i => i.Comments).FirstOrDefault();
                    if (image != null)
                    {
                        db.Comments.Add(new DbComment() { CreatedDate = DateTime.Now, DbImageId = image.Id, Id = "CommentToDelete", Text = "Text", UserId = newTestUser.Id });
                    }

                    db.Images.Add(new DbImage()
                    {
                        CaffFile = Array.Empty<byte>(), Id = "ImageToDelete", CaffFileName = "", Description = "",
                        IsDeleted = false, OwnerId = newTestUser.Id, Preview = "", SmallPreview = "", Title = "",
                        UploadTime = DateTime.Now, Buyers = new List<DbUserInfo>(), 
                        Comments = new List<DbComment>()
                    });
                    db.SaveChanges();
                }
            }
        });
    }
}