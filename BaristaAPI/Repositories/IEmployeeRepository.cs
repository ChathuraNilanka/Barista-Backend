using BaristaAPI.Models.Domain;

namespace BaristaAPI.Repositories
{
    public interface IEmployeeRepository
    {

        Task<List<Employee>> GetAllAsync(string cafeId);
        Task<Employee?> GetByIdAsync(string id);
        Task<Employee> CreateAsync(Employee employee);

        Task<Employee?> UpdateAsync(string id, Employee employee);
        Task<Employee?> DeleteAsync(string id);
    }
}
