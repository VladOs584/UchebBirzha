using Microsoft.EntityFrameworkCore;
using UchebBirzha.Domain.Entities;
using UchebBirzha.Domain.Interfaces;
using UchebBirzha.Infrastructure.Data;

namespace UchebBirzha.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IReadOnlyList<User>> GetExecutorsWithHighRatingAsync(decimal minRating)
        {
            return await _dbSet
                .Where(u => u.Rating >= minRating)
                .OrderByDescending(u => u.Rating)
                .ToListAsync();
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return !await _dbSet.AnyAsync(u => u.Email == email);
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(u => u.CreatedTasks)
                .Include(u => u.Bids)
                .ThenInclude(b => b.Task)
                .Include(u => u.ReceivedReviews)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}