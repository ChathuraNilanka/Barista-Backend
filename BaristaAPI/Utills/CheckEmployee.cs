using BaristaAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BaristaAPI.Utills
{
    public class CheckEmployee
    {
        private readonly APIDbContext dbContext;
        public CheckEmployee(APIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> IsEmployeeAssignedToCafeAsync(string employeeId)
        {
            return await dbContext.Employees.AnyAsync(e => e.Id == employeeId && e.CafeId != null);
        }
    }
}
