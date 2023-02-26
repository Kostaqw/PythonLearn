using Microsoft.EntityFrameworkCore;
using PythonLearn.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonLearn.DAL.Repositories
{
    public class ElementRepository : IElementRepository
    {
        ApplicationDbContext _context;

        public ElementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Element entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var element = _context.Elements.FirstOrDefault(x => x.Id == id);

            if(element == null) 
            {
                throw new ArgumentNullException("[ElementRepository] DeleteAsync(int id): the element isn't found");
            }

            _context.Elements.Remove(element);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Element entity)
        {
            var element = _context.Elements.FirstOrDefault(x => x.Id == entity.Id);
            if (element == null)
            {
                throw new ArgumentNullException("[ElementRepository] UpdateAsync(int id): the user isn't found");
            }

            if (!string.IsNullOrEmpty(entity.Name))
            {
                element.Name = entity.Name;
            }
            if (element.Type != null)
            { 
                element.Type = entity.Type;
            }
            if (!string.IsNullOrEmpty(entity.Discription))
            {
                element.Discription = entity.Discription;
            }
            if (!string.IsNullOrEmpty(entity.Answers))
            { 
                entity.Answers = entity.Answers;
            }
            element.LessonId = entity.LessonId;
            element.status = entity.status;


            await _context.SaveChangesAsync();
        }

        public async Task<List<Element>?> GetAllAsync()=>await _context.Elements.ToListAsync();

        public async Task<Element?> GetAsync(int id)=> await _context.Elements.FirstOrDefaultAsync(x => x.Id == id);

    }
}
