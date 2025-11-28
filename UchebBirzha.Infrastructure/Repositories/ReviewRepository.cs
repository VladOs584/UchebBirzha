using Microsoft.EntityFrameworkCore;
using UchebBirzha.Domain.Entities;
using UchebBirzha.Domain.Interfaces;
using UchebBirzha.Infrastructure.Data;

namespace UchebBirzha.Infrastructure.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IReadOnlyList<Review>> GetReviewsForExecutorAsync(int executorId)
        {
            return await _dbSet
                .Where(r => r.ExecutorId == executorId)
                .Include(r => r.Author)
                .Include(r => r.Task)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<decimal> GetAverageRatingForExecutorAsync(int executorId)
        {
            var average = await _dbSet
                .Where(r => r.ExecutorId == executorId)
                .AverageAsync(r => (decimal?)r.Rating) ?? 0;

            return Math.Round(average, 2);
        }

        public async Task<bool> HasReviewForTaskAsync(int taskId)
        {
            return await _dbSet
                .AnyAsync(r => r.TaskId == taskId);
        }

        public async Task<IReadOnlyList<Review>> GetRecentReviewsAsync(int count)
        {
            return await _dbSet
                .Include(r => r.Author)
                .Include(r => r.Executor)
                .Include(r => r.Task)
                .OrderByDescending(r => r.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public override async Task<Review> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(r => r.Author)
                .Include(r => r.Executor)
                .Include(r => r.Task)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}