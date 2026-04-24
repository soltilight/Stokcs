using Microsoft.EntityFrameworkCore;
using Stocks.Data;
using Stocks.Models;
using StocksOperation.Interfaces;

namespace StocksOperation.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public CommentRepository(ApplicationDBContext context)
        {
            _dbContext = context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
         await  _dbContext.Comments.AddAsync(commentModel);
            await _dbContext.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentModel = await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (commentModel == null)
            {
                return null;
            }
            _dbContext.Comments.Remove(commentModel);
            await _dbContext.SaveChangesAsync();
            return commentModel;

        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _dbContext.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _dbContext.Comments.FindAsync(id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var existingComment = await _dbContext.Comments.FindAsync(id);
            if (existingComment == null) 
            {
                return null;
            } 
            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;
            await _dbContext.SaveChangesAsync();

            return existingComment;
        }
    }
}

