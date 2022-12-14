using Backend.Dal;
using Backend.Exceptions;
using Backend.Extensions;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class CommentService : ICommentService
{
    public CommentService(AppDbContext context)
    {
        Context = context;
    }

    private AppDbContext Context { get; }

    public async Task DeleteCommentAsync(string commentId)
    {
        var comment = await Context.Comments.SingleOrDefaultAsync(c => c.Id == commentId);
        if (comment == null) throw new CommentNotFoundException();
        Context.Comments.Remove(comment);
        await Context.SaveChangesAsync();
    }

    public async Task<Comment> AddCommentAsync(string imageId, string userId, CommentRequest comment)
    {
        var image = await Context.Images.Include(i => i.Comments).SingleOrDefaultAsync(i => i.Id == imageId);
        if (image == null) throw new ImageNotFoundException();
        var dbComment = comment.ToEntity();
        dbComment.UserId = userId;
        image.Comments.Add(dbComment);
        await Context.SaveChangesAsync();
        return dbComment.ToModel();
    }
}