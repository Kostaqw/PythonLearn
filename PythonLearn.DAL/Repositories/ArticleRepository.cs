using Microsoft.EntityFrameworkCore;
using PythonLearn.DAL.Migrations;
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

        /// <summary>
        /// Удалить статью и теме по id статьи
        /// </summary>
        /// <param name="id">id статьи</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Статья не найдена</exception>
        public async Task DeleteAsync(int id)
        {
            var atricle = _context.Articles.FirstOrDefault(x => x.Id == id);
            
            if (atricle == null)
            {
                throw new ArgumentNullException("[ArticleRepository] DeleteAsync(int id): the article isn't found");
            }
           
            var title = _context.Titles.FirstOrDefault(x => x.Id == atricle.TitleId);
            _context.Titles.Remove(title);
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
            var result = await _context.Articles.Include(c=>c.Title).Include(d=>d.User).FirstOrDefaultAsync(x => x.Id == id);
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
            var result = _context.Articles.Include(c => c.Title).Include(d => d.User);
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
