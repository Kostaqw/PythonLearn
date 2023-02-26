using Microsoft.EntityFrameworkCore;
using PythonLearn.Domain.Entity;

namespace PythonLearn.DAL.Repositories
{
    public class ArticleCommentRepository : IArticleCommentRepository
    {
        ApplicationDbContext _context;

        public ArticleCommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ArticleComment entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var articleComment = _context.ArticleComments.FirstOrDefault(x => x.Id == id);

            if (articleComment == null)
            {
                throw new ArgumentNullException("[ArticleCommentRepository] DeleteAsync(int id): the article comment isn't found");
            }

            _context.ArticleComments.Remove(articleComment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ArticleComment entity)
        {
            var articleComment = _context.ArticleComments.FirstOrDefault(x => x.Id == entity.Id);
            if (articleComment == null)
            {
                throw new ArgumentNullException("[ArticleCommentRepository] UpdateAsync(int id): the article comment isn't found");
            }

            if (!string.IsNullOrEmpty(entity.CommentText))
            {
                articleComment.CommentText = entity.CommentText;
            }

            articleComment.UserId = entity.UserId;
            articleComment.ArticleId = entity.ArticleId;

            await _context.SaveChangesAsync();
        }

        public async Task<List<ArticleComment>?> GetAllAsync() => await _context.ArticleComments.ToListAsync();

        public async Task<ArticleComment?> GetAsync(int id) => await _context.ArticleComments.FirstOrDefaultAsync(x => x.Id == id);


    }
}
