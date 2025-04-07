using AutoMapper;
using AcmeSoft.Core.DTOs;
using AcmeSoft.Core.Interfaces;
using AcmeSoft.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcmeSoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public EmployeesController(
            IEmployeeRepository employeeRepository,
            IPersonRepository personRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await _employeeRepository.GetEmployeesWithPersonDetailsAsync();
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployeeWithPersonDetailsAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EmployeeDto>(employee));
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee(CreateEmployeeDto createEmployeeDto)
        {
            // Create Person entity
            var person = _mapper.Map<Person>(createEmployeeDto);

            // Create Employee entity
            var employee = _mapper.Map<Employee>(createEmployeeDto);

            using var transaction = _employeeRepository.BeginTransaction();

            try
            {
                // Add and save Person first to get its Id
                await _personRepository.AddAsync(person);
                await _personRepository.SaveChangesAsync();

                // Set the PersonId on Employee
                employee.PersonId = person.PersonId;

                // Add and save Employee
                await _employeeRepository.AddAsync(employee);
                await _employeeRepository.SaveChangesAsync();

                transaction.Commit();

                // Fetch the complete employee with person details
                var createdEmployee = await _employeeRepository.GetEmployeeWithPersonDetailsAsync(employee.EmployeeId);

                return CreatedAtAction(nameof(GetEmployee),
                    new { id = employee.EmployeeId },
                    _mapper.Map<EmployeeDto>(createdEmployee));
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeDto updateEmployeeDto)
        {
            if (id != updateEmployeeDto.EmployeeId)
            {
                return BadRequest();
            }

            var employee = await _employeeRepository.GetEmployeeWithPersonDetailsAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            var transaction = _employeeRepository.BeginTransaction();

            try
            {
                // Update Person entity
                if (employee.Person != null)
                {
                    _mapper.Map(updateEmployeeDto, employee.Person);
                    _personRepository.Update(employee.Person);
                }

                // Update Employee entity
                _mapper.Map(updateEmployeeDto, employee);
                _employeeRepository.Update(employee);

                await _employeeRepository.SaveChangesAsync();

                transaction.Commit();

                return NoContent();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _employeeRepository.Delete(employee);
            await _employeeRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}