using BaristaAPI.Models.Domain;

namespace BaristaAPI.Repositories
{
    public interface ICafeRepository
    {
        Task<List<Cafe>> GetAllAsync(string location);
        Task<Cafe?> GetByIdAsync(Guid id);
        Task<Cafe> CreateAsync(Cafe cafe);
        Task<Cafe?> UpdateAsync(Guid id, Cafe cafe);
        Task<Cafe?> DeleteAsync(Guid id);
    }
}
