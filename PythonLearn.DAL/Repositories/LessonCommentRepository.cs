using Microsoft.EntityFrameworkCore;
using PythonLearn.Domain.Entity;

namespace PythonLearn.DAL.Repositories
{
    public class LessonCommentRepository : ILessonCommentRepository
    {
        ApplicationDbContext _context;

        public LessonCommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(LessonComment entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var lessonComment = _context.LessonComments.FirstOrDefault(x => x.Id == id);

            if (lessonComment == null)
            {
                throw new ArgumentNullException("[LessonCommentRepository] DeleteAsync(int id): the lesson comment isn't found");
            }

            _context.LessonComments.Remove(lessonComment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LessonComment entity)
        {
            var lessonComment = _context.LessonComments.FirstOrDefault(x => x.Id == entity.Id);
            if (lessonComment == null)
            {
                throw new ArgumentNullException("[LessonCommentRepository] UpdateAsync(int id): the lesson comment isn't found");
            }

            if (!string.IsNullOrEmpty(entity.CommentText))
            {
                lessonComment.CommentText = entity.CommentText;
            }

            lessonComment.UserId = entity.UserId;
            lessonComment.LessonId = entity.LessonId;

            await _context.SaveChangesAsync();
        }

        public async Task<LessonComment> GetAsync(int id)
        {
            var result = await _context.LessonComments.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new ArgumentNullException("[LessonCommentRepository] GetAsync(int id): the lesson comment isn't found");
            }
            else
            {
                return result;
            }
        }

        public IQueryable<LessonComment> GetAllAsync()
        {
            var result = _context.LessonComments;
            if (result == null)
            {
                throw new ArgumentNullException("[LessonCommentRepository] GetAllAsync(): the lesson comments arn't found");
            }
            else
            {
                return result;
            }
        }


    }
}
