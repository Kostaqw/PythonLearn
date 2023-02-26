using Microsoft.EntityFrameworkCore;
using PythonLearn.Domain.Entity;

namespace PythonLearn.DAL.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Role entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Id == id);

            if (role == null)
            {
                throw new ArgumentNullException("[RoleRepository] DeleteAsync(int id): the role isn't found");
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Role entity)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Id == entity.Id);
            if (role == null)
            {
                throw new ArgumentNullException("[RoleRepository] UpdateAsync(int id): the role isn't found");
            }

            if (!string.IsNullOrEmpty(entity.Name))
            {
                role.Name = entity.Name;
            }
            if (!string.IsNullOrEmpty(entity.ShortDescription))
            {
                role.ShortDescription = entity.ShortDescription;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<Role>?> GetAllAsync() => await _context.Roles.ToListAsync();

        public async Task<Role?> GetAsync(int id) => await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
    }
}
