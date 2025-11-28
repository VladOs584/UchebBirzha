using UchebBirzha.Domain.Entities;

namespace UchebBirzha.Domain.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<IReadOnlyList<Review>> GetReviewsForExecutorAsync(int executorId);
        Task<decimal> GetAverageRatingForExecutorAsync(int executorId);
        Task<bool> HasReviewForTaskAsync(int taskId);
    }
}