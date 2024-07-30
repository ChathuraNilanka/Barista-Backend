using BaristaAPI.Data;
using BaristaAPI.Models.Domain;
using BaristaAPI.Utills;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BaristaAPI.Repositories
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly APIDbContext dbContext;
        public SQLEmployeeRepository(APIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Employee> CreateAsync(Employee employee)
        {
            string employeeId;
            do
            {
                employeeId = EmployeeIdGenerator.GenerateEmployeeId();
            }
            while (await dbContext.Employees.AnyAsync(e => e.Id == employeeId));

            employee.Id = employeeId;

            if (!string.IsNullOrEmpty(employee.CafeId.ToString()))
            {
                employee.StartDate = DateTime.Now;
            }

            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> DeleteAsync(string id)
        {
            var existEmployee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (existEmployee == null)
            {
                return null;
            }
            dbContext.Employees.Remove(existEmployee);
            await dbContext.SaveChangesAsync();

            return existEmployee;
        }

        public async Task<List<Employee>> GetAllAsync(string cafe)
        {
            var employees =  dbContext.Employees.Include("Cafe").AsQueryable();
            if (!string.IsNullOrEmpty(cafe))
            {
                employees = employees.Where(e => e.Cafe.Id.ToString() == cafe);
            }
            return  await employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(string id)
        {
            return await dbContext.Employees.Include("Cafe").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Employee?> UpdateAsync(string id, Employee employee)
        {
            var existEmployee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (existEmployee == null)
            {
                return null;
            }
            
            existEmployee.Name = employee.Name;
            existEmployee.EmailAddress = employee.EmailAddress;
            existEmployee.PhoneNumber = employee.PhoneNumber;
            existEmployee.Gender = employee.Gender;
            existEmployee.StartDate = employee.StartDate;
            existEmployee.CafeId = employee.CafeId;

            await dbContext.SaveChangesAsync();
            return existEmployee;
        }
    }
}
