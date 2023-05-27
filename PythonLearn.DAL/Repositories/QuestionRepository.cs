using Microsoft.EntityFrameworkCore;
using PythonLearn.DAL.@interface;
using PythonLearn.Domain.Entity;

namespace PythonLearn.DAL.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Question entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var question = await _context.Questions.FindAsync(id);

            if (question == null)
            {
                throw new ArgumentNullException("[QuestionRepository] DeleteAsync(int id): the question isn't found");
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Question entity)
        {
            var question = await _context.Questions.FindAsync(entity.Id);
            if (question == null)
            {
                throw new ArgumentNullException("[QuestionRepository] UpdateAsync(int id): the question isn't found");
            }

            if (!string.IsNullOrEmpty(entity.Text))
            {
                question.Text = entity.Text;
            }
            if (!string.IsNullOrEmpty(entity.CorrectAnswer))
            {
                question.CorrectAnswer = entity.CorrectAnswer;
            }
            if (question.Answers != null)
            {
                question.Answers = entity.Answers;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Question> GetAsync(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                throw new ArgumentNullException("[QuestionRepository] GetAsync(int id): the question isn't found");
            }
            else
            {
                return question;
            }
        }

        public IQueryable<Question> GetAllAsync()
        {
            var questions = _context.Questions;
            if (questions == null)
            {
                throw new ArgumentNullException("[QuestionRepository] GetAllAsync(): the questions aren't found");
            }
            else
            {
                return questions;
            }
        }
    }
}
