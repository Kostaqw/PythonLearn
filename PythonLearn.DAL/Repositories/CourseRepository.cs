﻿using Microsoft.EntityFrameworkCore;
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

            await _context.SaveChangesAsync();
        }
        public async Task<Course?> GetAsync(int id)
        {
            var result = await _context.Courses
                .Include(c => c.Lessons).ThenInclude(a => a.Tests).ThenInclude(q=>q.Questions)
                .Include(c => c.Lessons).ThenInclude(e => e.Lectures)
                .Include(c => c.Lessons).ThenInclude(a => a.Practices)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new ArgumentNullException("[CourseRepository] GetAsync(int id): the course isn't found");
            }
            else
            {
                return result;
            }
        } 

        public IQueryable<Course> GetAllAsync()
        {
            var result = _context.Courses.Include(c=>c.Lessons);
            if (result == null)
            {
                throw new ArgumentNullException("[CourseRepository] GetAllAsync(): the courses arn't found");
            }
            else
            {
                return result;
            }
        }
    }
}
