﻿using Microsoft.EntityFrameworkCore;
using PythonLearn.Domain.Entity;

namespace PythonLearn.DAL.Repositories
{
    public class SolutionRepository : ISolutionRepository
    {
        ApplicationDbContext _context;

        public SolutionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Solution entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var solution = _context.Solutions.FirstOrDefault(x => x.Id == id);

            if (solution == null)
            {
                throw new ArgumentNullException("[SolutionRepository] DeleteAsync(int id): the solution isn't found");
            }

            _context.Solutions.Remove(solution);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Solution entity)
        {
            var solution = _context.Solutions.FirstOrDefault(x => x.Id == entity.Id);
            if (solution == null)
            {
                throw new ArgumentNullException("[SolutionRepository] UpdateAsync(int id): the solution isn't found");
            }

            if(!string.IsNullOrEmpty(entity.SolutionText))
            {
                solution.SolutionText= entity.SolutionText;
            }
            
            await _context.SaveChangesAsync();
        }

        public async Task<List<Solution>?> GetAllAsync() => await _context.Solutions.ToListAsync();

        public async Task<Solution?> GetAsync(int id) => await _context.Solutions.FirstOrDefaultAsync(x => x.Id == id);

    }
}
