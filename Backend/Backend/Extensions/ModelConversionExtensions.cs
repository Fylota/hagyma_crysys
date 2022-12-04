using Backend.Dal.Entities;
using Backend.Models;

namespace Backend.Extensions;

public static class ModelConversionExtensions
{
    public static CaffDetails ToDetails(this DbImage image)
    {
        return new CaffDetails
        {
            Id = image.Id,
            Title = image.Title,
            Preview = image.Preview,
            Description = image.Description,
            Comments = image.Comments?.Where(c => c != null).Select(c => c.ToModel()).ToList() ?? new List<Comment>(),
            UploadTime = image.UploadTime,
            OwnerId = image.OwnerId
        };
    }

    public static CaffItem ToItem(this DbImage image)
    {
        return new CaffItem
        {
            Id = image.Id,
            Title = image.Title,
            Preview = image.SmallPreview
        };
    }

    public static Comment ToModel(this DbComment comment)
    {
        return new Comment
        {
            Id = comment.Id,
            Content = comment.Text,
            CreationTime = comment.CreatedDate,
            CreatorName = comment.User?.UserName ?? string.Empty
        };
    }

    public static DbComment ToEntity(this CommentRequest comment)
    {
        return new DbComment
        {
            CreatedDate = DateTime.Now,
            Text = comment.Content
        };
    }

    public static DbImage ToEntity(this CaffUploadRequest request)
    {
        return new DbImage
        {
            Title = request.Title,
            Description = request.Description
        };
    }

    public static User ToModel(this DbUserInfo dbUser)
    {
        return new User
        {
            Email = dbUser.Email,
            Id = dbUser.Id,
            Name = dbUser.UserName,
            RegistrationDate = dbUser.RegistrationDate,
            IsDeleted = dbUser.LockoutEnabled && dbUser.LockoutEnd != null
        };
    }
}