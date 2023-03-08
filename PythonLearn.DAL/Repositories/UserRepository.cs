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
            user.avatar = entity.avatar;

            await _context.SaveChangesAsync();
        }

        public async Task<List<User>?> GetAllAsync()=>await _context.Users.ToListAsync();

        public async Task<User?> GetAsync(int id)=> await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<User>?> GetByNameAsync(string name) => await _context.Users.Where(x => x.Name == name).ToListAsync();
        public async Task<List<User>?> GetBySecondNameAsync(string secondName) => await _context.Users.Where(x => x.SecondName== secondName).ToListAsync();

    }
}
