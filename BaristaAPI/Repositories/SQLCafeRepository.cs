using BaristaAPI.Data;
using BaristaAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BaristaAPI.Repositories
{
    public class SQLCafeRepository : ICafeRepository
    {
        private readonly APIDbContext dbContext;
        public SQLCafeRepository(APIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Cafe> CreateAsync(Cafe cafe)
        {
            await dbContext.Cafes.AddAsync(cafe);
            await dbContext.SaveChangesAsync();
            return cafe;
        }

        public async Task<Cafe?> DeleteAsync(Guid id)
        {
            var existCafe = await dbContext.Cafes.FirstOrDefaultAsync(x => x.Id == id);
            if (existCafe == null)
            {
                return null;
            }
            dbContext.Cafes.Remove(existCafe);
            await dbContext.SaveChangesAsync();

            return existCafe;
        }

        public async Task<List<Cafe>> GetAllAsync(string location)
        {
            var cafes = dbContext.Cafes.Include(c => c.Employees).AsQueryable();
            if (!string.IsNullOrEmpty(location))
            {
                cafes = cafes.Where(e => e.Location == location);
            }
            return await cafes
                .OrderByDescending(c => c.Employees.Count)
                .ToListAsync();
        }

        public async Task<Cafe?> GetByIdAsync(Guid id)
        {
            return await dbContext.Cafes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Cafe?> UpdateAsync(Guid id, Cafe cafe)
        {
            var existCafe = await dbContext.Cafes.FirstOrDefaultAsync(x => x.Id == id);
            if (existCafe == null)
            {
                return null;
            }

            existCafe.Name = cafe.Name;
            existCafe.Description = cafe.Description;
            existCafe.Logo = cafe.Logo;
            existCafe.Location = cafe.Location;

            await dbContext.SaveChangesAsync();
            return existCafe;
        }

    }
}
