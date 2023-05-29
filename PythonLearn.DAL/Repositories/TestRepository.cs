using Microsoft.EntityFrameworkCore;
using PythonLearn.DAL.@interface;
using PythonLearn.Domain.Entity;

namespace PythonLearn.DAL.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly ApplicationDbContext _context;

        public TestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Test entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var test = await _context.Tests.FirstOrDefaultAsync(x => x.Id == id);

            if (test == null)
            {
                throw new ArgumentNullException("[TestRepository] DeleteAsync(int id): the test isn't found");
            }

            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Test entity)
        {
            var test = await _context.Tests.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (test == null)
            {
                throw new ArgumentNullException("[TestRepository] UpdateAsync(int id): the test isn't found");
            }

            if (entity.Questions != null)
            {
                test.Questions = entity.Questions;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Test> GetAsync(int id)
        {
            var result = await _context.Tests.Include(c=>c.Questions).FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new ArgumentNullException("[TestRepository] GetAsync(int id): the test isn't found");
            }
            else
            {
                return result;
            }
        }

        public IQueryable<Test> GetAllAsync()
        {
            var result = _context.Tests.Include(c=>c.Questions);
            if (result == null)
            {
                throw new ArgumentNullException("[TestRepository] GetAllAsync(): the tests aren't found");
            }
            else
            {
                return result;
            }
        }
    }
}
