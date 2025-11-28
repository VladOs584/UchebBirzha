using Microsoft.EntityFrameworkCore;
using UchebBirzha.Domain.Entities;
using UchebBirzha.Domain.Interfaces;
using UchebBirzha.Infrastructure.Data;

namespace UchebBirzha.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<IReadOnlyList<Category>> GetPopularCategoriesAsync(int count)
        {
            return await _dbSet
                .OrderByDescending(c => c.Tasks.Count)
                .Take(count)
                .ToListAsync();
        }

        public override async Task<Category> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(c => c.Tasks)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}