using Microsoft.EntityFrameworkCore;
using PythonLearn.DAL.@interface;
using PythonLearn.Domain.Entity;

namespace PythonLearn.DAL.Repositories
{
    public class PracticeRepository : IPracticeRepository
    {
        private readonly ApplicationDbContext _context;

        public PracticeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Practice entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var practice = await _context.Practices.FirstOrDefaultAsync(x => x.Id == id);

            if (practice == null)
            {
                throw new ArgumentNullException("[PracticeRepository] DeleteAsync(int id): the practice isn't found");
            }

            _context.Practices.Remove(practice);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Practice entity)
        {
            var practice = await _context.Practices.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (practice == null)
            {
                throw new ArgumentNullException("[PracticeRepository] UpdateAsync(int id): the practice isn't found");
            }

            if (!string.IsNullOrEmpty(entity.Question))
            {
                practice.Question = entity.Question;
            }
            if (!string.IsNullOrEmpty(entity.CorrectAnswer))
            {
                practice.Question = entity.Question;
            }
            practice.IsComplited = entity.IsComplited;

            await _context.SaveChangesAsync();
        }

        public async Task<Practice> GetAsync(int id)
        {
            var result = await _context.Practices.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new ArgumentNullException("[PracticeRepository] GetAsync(int id): the practice isn't found");
            }
            else
            {
                return result;
            }
        }

        public IQueryable<Practice> GetAllAsync()
        {
            var result = _context.Practices;
            if (result == null)
            {
                throw new ArgumentNullException("[PracticeRepository] GetAllAsync(): the practices aren't found");
            }
            else
            {
                return result;
            }
        }
    }

}
