using Microsoft.EntityFrameworkCore;
using PythonLearn.Domain.Entity;

namespace PythonLearn.DAL.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        ApplicationDbContext _context;

        public LessonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Lesson entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var lesson = _context.Lessons.FirstOrDefault(x => x.Id == id);

            if (lesson == null)
            {
                throw new ArgumentNullException("[LessonRepository] DeleteAsync(int id): the lesson isn't found");
            }

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Lesson entity)
        {
            var lesson = _context.Lessons.FirstOrDefault(x => x.Id == entity.Id);
            if (lesson == null)
            {
                throw new ArgumentNullException("[LessonRepository] UpdateAsync(int id): the lesson isn't found");
            }

            if (!string.IsNullOrEmpty(entity.Name))
            {
                lesson.Name = entity.Name;
            }
            lesson.status = entity.status;

            await _context.SaveChangesAsync();
        }

        public async Task<Lesson> GetAsync(int id)
        {
            var result = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new ArgumentNullException("[LessonRepository] GetAsync(int id): the lesson isn't found");
            }
            else
            {
                return result;
            }
        }

        public IQueryable<Lesson> GetAllAsync()
        {
            var result = _context.Lessons.Include(c=>c.Tests).Include(d=>d.Lectures).Include(e=>e.Practices);
            if (result == null)
            {
                throw new ArgumentNullException("[LessonRepository] GetAllAsync(): the lessons arn't found");
            }
            else
            {
                return result;
            }
        }


    }
}
