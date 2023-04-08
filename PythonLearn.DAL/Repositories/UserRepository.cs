using Microsoft.EntityFrameworkCore;
using PythonLearn.Domain.Entity;

namespace PythonLearn.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(User entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if(user == null) 
            {
                throw new ArgumentNullException("[UserRepository] DeleteAsync(int id): the user isn't found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == entity.Id);
            if (user == null)
            {
                throw new ArgumentNullException("[UserRepository] UpdateAsync(int id): the user isn't found");
            }

            if (!string.IsNullOrEmpty(entity.Name))
            {
                user.Name = entity.Name;
            }
            if (!string.IsNullOrEmpty(entity.SecondName))
            {
                user.SecondName = entity.SecondName;
            }
            if (entity.BirthDay != null)
            {
                user.BirthDay = entity.BirthDay;
            }
            if (!string.IsNullOrEmpty(entity.Email))
            {
                user.Email = entity.Email;
            }
            if (!string.IsNullOrEmpty(entity.Password))
            {
                user.Password = entity.Password;
            }
            if (!string.IsNullOrEmpty(entity.AboutMe))
            {
                user.AboutMe = entity.AboutMe;
            }
            if (entity.avatar != null)
            {
                user.avatar = entity.avatar;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new ArgumentNullException("[UserRepository] GetAsync(int id): the user isn't found");
            }
            else
            {
                return result;
            }
        }

        public IQueryable<User> GetAllAsync()
        {
            var result = _context.Users;
            if (result == null)
            {
                throw new ArgumentNullException("[UserRepository] GetAllAsync(): the users arn't found");
            }
            else
            {
                return result;
            }
        }

    }
}
