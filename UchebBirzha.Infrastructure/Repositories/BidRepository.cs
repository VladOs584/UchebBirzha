using Microsoft.EntityFrameworkCore;
using UchebBirzha.Domain.Entities;
using UchebBirzha.Domain.Interfaces;
using UchebBirzha.Infrastructure.Data;

namespace UchebBirzha.Infrastructure.Repositories
{
    public class BidRepository : BaseRepository<Bid>, IBidRepository
    {
        public BidRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IReadOnlyList<Bid>> GetBidsForTaskAsync(int taskId)
        {
            return await _dbSet
                .Where(b => b.TaskId == taskId)
                .Include(b => b.Executor)
                .OrderBy(b => b.ProposedPrice)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Bid>> GetBidsByExecutorAsync(int executorId)
        {
            return await _dbSet
                .Where(b => b.ExecutorId == executorId)
                .Include(b => b.Task)
                .ThenInclude(t => t.Customer)
                .Include(b => b.Task)
                .ThenInclude(t => t.Category)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<Bid> GetBidByTaskAndExecutorAsync(int taskId, int executorId)
        {
            return await _dbSet
                .Include(b => b.Task)
                .Include(b => b.Executor)
                .FirstOrDefaultAsync(b => b.TaskId == taskId && b.ExecutorId == executorId);
        }

        public async Task<bool> HasExecutorBidForTaskAsync(int taskId, int executorId)
        {
            return await _dbSet
                .AnyAsync(b => b.TaskId == taskId && b.ExecutorId == executorId);
        }

        public override async Task<Bid> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(b => b.Task)
                .ThenInclude(t => t.Customer)
                .Include(b => b.Executor)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}