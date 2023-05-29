using Microsoft.EntityFrameworkCore;
using PythonLearn.DAL.@interface;
using PythonLearn.Domain.Entity;

namespace PythonLearn.DAL.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ApplicationDbContext _context;

        public AnswerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Answer entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var answer = await _context.Answers.FirstOrDefaultAsync(x => x.Id == id);

            if (answer == null)
            {
                throw new ArgumentNullException("[AnswerRepository] DeleteAsync(int id): the answer isn't found");
            }

            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Answer entity)
        {
            var answer = await _context.Answers.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (answer == null)
            {
                throw new ArgumentNullException("[AnswerRepository] UpdateAsync(Answer entity): the answer isn't found");
            }

            answer.Text = entity.Text;
            answer.IsCorrect = entity.IsCorrect;

            await _context.SaveChangesAsync();
        }

        public async Task<Answer> GetAsync(int id)
        {
            var answer = await _context.Answers.FirstOrDefaultAsync(x => x.Id == id);

            if (answer == null)
            {
                throw new ArgumentNullException("[AnswerRepository] GetAsync(int id): the answer isn't found");
            }

            return answer;
        }

        public IQueryable<Answer> GetAllAsync()
        {
            return _context.Answers.AsQueryable();
        }
    }
}
