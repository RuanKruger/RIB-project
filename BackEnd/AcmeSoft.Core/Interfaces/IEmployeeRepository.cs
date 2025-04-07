using AcmeSoft.Core.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace AcmeSoft.Core.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployeesWithPersonDetailsAsync();
        Task<Employee?> GetEmployeeWithPersonDetailsAsync(int id);
        IDbContextTransaction BeginTransaction();
    }
}