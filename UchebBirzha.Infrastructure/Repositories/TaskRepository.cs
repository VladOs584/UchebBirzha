using Microsoft.EntityFrameworkCore;
using UchebBirzha.Domain.Entities;
using UchebBirzha.Domain.Enums;
using UchebBirzha.Domain.Interfaces;
using UchebBirzha.Infrastructure.Data;

namespace UchebBirzha.Infrastructure.Repositories
{
    public class TaskRepository : BaseRepository<Domain.Entities.Task>, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IReadOnlyList<Domain.Entities.Task>> GetOpenTasksAsync()
        {
            return await _dbSet
                .Where(t => t.Status == TaskWorkStatus.Open)
                .Include(t => t.Customer)
                .Include(t => t.Category)
                .Include(t => t.Bids)
                .ThenInclude(b => b.Executor)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Domain.Entities.Task>> GetTasksByCustomerAsync(string customerId)
        {
            return await _dbSet
                .Where(t => t.CustomerId == customerId)
                .Include(t => t.Category)
                .Include(t => t.Executor)
                .Include(t => t.Bids)
                .ThenInclude(b => b.Executor)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Domain.Entities.Task>> GetTasksByExecutorAsync(string executorId)
        {
            return await _dbSet
                .Where(t => t.ExecutorId == executorId)
                .Include(t => t.Customer)
                .Include(t => t.Category)
                .Include(t => t.Bids)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Domain.Entities.Task>> GetTasksByCategoryAsync(int categoryId)
        {
            return await _dbSet
                .Where(t => t.CategoryId == categoryId && t.Status == TaskWorkStatus.Open)
                .Include(t => t.Customer)
                .Include(t => t.Bids)
                .ThenInclude(b => b.Executor)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Domain.Entities.Task>> GetOverdueTasksAsync()
        {
            return await _dbSet
                .Where(t => t.Deadline < DateTime.UtcNow && t.Status == TaskWorkStatus.InProgress)
                .Include(t => t.Customer)
                .Include(t => t.Executor)
                .ToListAsync();
        }

        public override async Task<Domain.Entities.Task> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(t => t.Customer)
                .Include(t => t.Executor)
                .Include(t => t.Category)
                .Include(t => t.Bids)
                .ThenInclude(b => b.Executor)
                .Include(t => t.Attachments)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IReadOnlyList<Domain.Entities.Task>> GetTasksWithBidsAsync()
        {
            return await _dbSet
                .Where(t => t.Bids.Any())
                .Include(t => t.Bids)
                .ThenInclude(b => b.Executor)
                .Include(t => t.Customer)
                .ToListAsync();
        }
    }
}