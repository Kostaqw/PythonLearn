using Microsoft.EntityFrameworkCore;
using PythonLearn.Domain.Entity;

namespace PythonLearn.DAL.Repositories
{
    public class TitleRepository : ITitleRepository
    {
        ApplicationDbContext _context;

        public TitleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Title entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var title = _context.Titles.FirstOrDefault(x => x.Id == id);

            if (title == null)
            {
                throw new ArgumentNullException("[TitleRepository] DeleteAsync(int id): the title isn't found");
            }

            _context.Titles.Remove(title);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Title entity)
        {
            var title = _context.Titles.FirstOrDefault(x => x.Id == entity.Id);
            if (title == null)
            {
                throw new ArgumentNullException("[TitleRepository] UpdateAsync(int id): the title isn't found");
            }

            if (!string.IsNullOrEmpty(entity.Name))
            {
                title.Name = entity.Name;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Title> GetAsync(int id)
        {
            var result = await _context.Titles.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new ArgumentNullException("[TitleRepository] GetAsync(int id): the title isn't found");
            }
            else
            {
                return result;
            }
        }

        public IQueryable<Title> GetAllAsync()
        {
            var result = _context.Titles;
            if (result == null)
            {
                throw new ArgumentNullException("[TitleRepository] GetAllAsync(): the titles arn't found");
            }
            else
            {
                return result;
            }
        }
    }
}
