using Backend.Dal.Entities;
using Backend.Models;

namespace Backend.Extensions
{
    public static class ModelConversionExtensions
    {
        public static User ToModel(this DbUserInfo dbUser)
        {
            return new User()
            {
                Email = dbUser.Email,
                Id = dbUser.Id,
                Name = dbUser.UserName
            };
        }

        public static CaffItem ToItem(this DbImage image)
        {
            return new CaffItem()
            {
                Id = image.Id,
                Title = image.Title,
                Preview = image.Preview,
            };
        }

        public static CaffDetails ToDetails(this DbImage image)
        {
            return new CaffDetails()
            {
                Id = image.Id,
                Title = image.Title,
                Preview = image.Preview,
                Description = image.Description,
                Comments = image.Comments.Select(c => c.ToModel()).ToList()
            };
        }

        public static Comment ToModel(this DbComment comment)
        {
            return new Comment()
            {
                Id = comment.Id,
                Content = comment.Text,
                CreationTime = comment.CreatedDate
            };
        }
    }
}
