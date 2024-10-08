using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDBContext _dbContext;

    public CommentRepository(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Comment>> GetAllAsync()
    {
        return await _dbContext.Comments.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Comment?> CreateAsync(Comment commentModel)
    {
        await _dbContext.AddAsync(commentModel);
        await _dbContext.SaveChangesAsync();

        return commentModel;
    }

    public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
    {
        var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

        if (comment == null)
        {
            return null;
        }

        comment.Title = commentModel.Title;
        comment.Content = commentModel.Content;

        await _dbContext.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

        if (comment == null)
        {
            return null;
        }

        _dbContext.Comments.Remove(comment);
        await _dbContext.SaveChangesAsync();

        return comment;
    }
}