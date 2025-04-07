using AcmeSoft.Core.Interfaces;
using AcmeSoft.Core.Models;
using AcmeSoft.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace AcmeSoft.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employee.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employee.FindAsync(id);
        }

        public async Task AddAsync(Employee entity)
        {
            await _context.Employee.AddAsync(entity);
        }

        public void Update(Employee entity)
        {
            _context.Employee.Update(entity);
        }

        public void Delete(Employee entity)
        {
            _context.Employee.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesWithPersonDetailsAsync()
        {
            return await _context.Employee
                .Include(e => e.Person)
                .ToListAsync();
        }

        public async Task<Employee?> GetEmployeeWithPersonDetailsAsync(int id)
        {
            return await _context.Employee
                .Include(e => e.Person)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
    }
}