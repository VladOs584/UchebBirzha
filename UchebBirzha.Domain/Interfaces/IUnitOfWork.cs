namespace UchebBirzha.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskRepository Tasks { get; }
        IUserRepository Users { get; }
        IBidRepository Bids { get; }
        ICategoryRepository Categories { get; }
        IReviewRepository Reviews { get; }

        Task<int> SaveChangesAsync();
    }
}