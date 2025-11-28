using UchebBirzha.Domain.Interfaces;
using UchebBirzha.Infrastructure.Data;

namespace UchebBirzha.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Tasks = new TaskRepository(_context);
            Bids = new BidRepository(_context);
            Categories = new CategoryRepository(_context);
            Reviews = new ReviewRepository(_context);
        }

        public IUserRepository Users { get; }
        public ITaskRepository Tasks { get; }
        public IBidRepository Bids { get; }
        public ICategoryRepository Categories { get; }
        public IReviewRepository Reviews { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}