using Backend.Models;

namespace Backend.Services.Interfaces;

public interface ICommentService
{
    public Task<Comment> AddCommentAsync(string imageId, string userId, CommentRequest comment);
    public Task DeleteCommentAsync(string commentId);
}