using AcmeSoft.Core.Interfaces;
using AcmeSoft.Core.Models;
using AcmeSoft.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AcmeSoft.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _context;

        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Person.ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await _context.Person.FindAsync(id);
        }

        public async Task AddAsync(Person entity)
        {
            await _context.Person.AddAsync(entity);
        }

        public void Update(Person entity)
        {
            _context.Person.Update(entity);
        }

        public void Delete(Person entity)
        {
            _context.Person.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}