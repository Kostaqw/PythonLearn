using Microsoft.EntityFrameworkCore;
using PythonLearn.DAL.@interface;
using PythonLearn.Domain.Entity;

namespace PythonLearn.DAL.Repositories
{
    public class LectureRepository : ILectureRepository
    {
        private readonly ApplicationDbContext _context;

        public LectureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Lecture entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var lecture = await _context.Lectures.FirstOrDefaultAsync(x => x.Id == id);

            if (lecture == null)
            {
                throw new ArgumentNullException("[LectureRepository] DeleteAsync(int id): the lecture isn't found");
            }

            _context.Lectures.Remove(lecture);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Lecture entity)
        {
            var lecture = await _context.Lectures.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (lecture == null)
            {
                throw new ArgumentNullException("[LectureRepository] UpdateAsync(int id): the lecture isn't found");
            }

            if (!string.IsNullOrEmpty(entity.Text))
            {
                lecture.Text = entity.Text;
            }
            lecture.IsComplited = entity.IsComplited;

            await _context.SaveChangesAsync();
        }

        public async Task<Lecture> GetAsync(int id)
        {
            var result = await _context.Lectures.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new ArgumentNullException("[LectureRepository] GetAsync(int id): the lecture isn't found");
            }
            else
            {
                return result;
            }
        }

        public IQueryable<Lecture> GetAllAsync()
        {
            var result = _context.Lectures;
            if (result == null)
            {
                throw new ArgumentNullException("[LectureRepository] GetAllAsync(): the lectures aren't found");
            }
            else
            {
                return result;
            }
        }
    }
}
