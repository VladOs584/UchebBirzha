using UchebBirzha.Domain.Entities;

namespace UchebBirzha.Domain.Interfaces
{
    public interface IBidRepository : IRepository<Bid>
    {
        Task<IReadOnlyList<Bid>> GetBidsForTaskAsync(int taskId);
        Task<IReadOnlyList<Bid>> GetBidsByExecutorAsync(int executorId);
        Task<Bid> GetBidByTaskAndExecutorAsync(int taskId, int executorId);
    }
}