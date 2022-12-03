using System.IO.Compression;
using Backend.Dal.Entities;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Backend.Dal;

public class AppDbContext : ApiAuthorizationDbContext<DbUserInfo>
{
    public AppDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(
        options, operationalStoreOptions)
    {
    }

    public DbSet<DbComment> Comments { get; set; } = null!;

    public DbSet<DbImage> Images { get; set; } = null!;

    // ReSharper disable once UnusedMember.Global
    public DbSet<Log> Logs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<DbUserInfo>().HasMany(u => u.PurchasedImages).WithMany(i => i.Buyers);
        builder.Entity<DbUserInfo>().Property(u => u.RegistrationDate).HasDefaultValue(DateTime.Now);
        builder.Entity<DbImage>().Property(i => i.IsDeleted).HasDefaultValue(false);
        builder.Entity<DbImage>().HasQueryFilter(p => !p.IsDeleted);
        builder.Entity<DbImage>().Property(i => i.SmallPreview).HasDefaultValue("");
        builder.Entity<DbComment>().HasOne(c => c.User).WithMany();
        SeedData(builder);

        base.OnModelCreating(builder);
    }

    private static void SeedData(ModelBuilder builder)
    {
        var dummyUser = new DbUserInfo
        {
            UserName = "dummy",
            Email = "dummy@dummy.com",
            Id = "ID"
        };
        builder.Entity<DbUserInfo>().HasData(dummyUser);

        var directory = Directory.GetCurrentDirectory();

        var caffFile = Directory.GetFiles(directory, "*.caff").FirstOrDefault()!;
        var previewFile = Directory.GetFiles(directory, "*.jpg").FirstOrDefault()!;


        var caff = new FileInfo(caffFile);
        var image = new DbImage
        {
            Id = "ImageID",
            Description = "Description",
            Title = "Title",
            UploadTime = DateTime.Now,
            OwnerId = dummyUser.Id,
            CaffFileName = caff.Name
        };
        using (var memStream = new MemoryStream())
        {
            using (var dStream = new DeflateStream(memStream, CompressionLevel.Optimal))
            {
                var bytes = File.ReadAllBytes(caffFile);
                dStream.WriteAsync(bytes, 0, bytes.Length).Wait();
            }

            image.CaffFile = memStream.ToArray();
        }

        image.Preview = Convert.ToBase64String(File.ReadAllBytes(previewFile));
        image.SmallPreview = image.Preview;

        builder.Entity<DbImage>().HasData(image);

        var comment = new DbComment
        {
            Id = "CommentID",
            UserId = dummyUser.Id,
            DbImageId = image.Id,
            CreatedDate = DateTime.Now,
            Text = "Test comment"
        };

        builder.Entity<DbComment>().HasData(comment);
    }
}