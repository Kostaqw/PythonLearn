using Microsoft.EntityFrameworkCore;
using PythonLearn.Domain.Entity;

namespace PythonLearn.DAL.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Course entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = _context.Courses.FirstOrDefault(x => x.Id == id);

            if (course == null)
            {
                throw new ArgumentNullException("[CourseRepository] DeleteAsync(int id): the course isn't found");
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course entity)
        {
            var course = _context.Courses.FirstOrDefault(x => x.Id == entity.Id);
            if (course == null)
            {
                throw new ArgumentNullException("[CourseRepository] UpdateAsync(int id): the course isn't found");
            }

            course.LessonId = entity.LessonId;
            course.userId = entity.userId;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Course>?> GetAllAsync() => await _context.Courses.ToListAsync();

        public async Task<Course?> GetAsync(int id) => await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);

    }
}
