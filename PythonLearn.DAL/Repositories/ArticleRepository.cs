using Microsoft.EntityFrameworkCore;
using PythonLearn.Domain.Entity;

namespace PythonLearn.DAL.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Article entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var atricle = _context.Articles.FirstOrDefault(x => x.Id == id);

            if (atricle == null)
            {
                throw new ArgumentNullException("[ArticleRepository] DeleteAsync(int id): the article isn't found");
            }

            _context.Articles.Remove(atricle);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Article entity)
        {
            var article = _context.Articles.FirstOrDefault(x => x.Id == entity.Id);
            if (article == null)
            {
                throw new ArgumentNullException("[ArticleRepository] UpdateAsync(int id): the article isn't found");
            }
            if (!string.IsNullOrEmpty(article.ArticleText))
            { 
                entity.ArticleText = article.ArticleText;
            }
            article.TitleId = entity.TitleId;
            article.UserId = entity.UserId;

            await _context.SaveChangesAsync();
        }

        public async Task<Article?> GetAsync(int id)
        {
            var result = await _context.Articles.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new ArgumentNullException("[ArticleRepository] GetAsync(int id): the article isn't found");
            }
            else
            {
                return result;
            }
        }

        public IQueryable<Article> GetAllAsync()
        {
            var result = _context.Articles;
            if (result == null)
            {
                throw new ArgumentNullException("[ArticleRepository] GetAllAsync(): the articles arn't found");
            }
            else
            {
                return result;
            }
        }
    }
}
