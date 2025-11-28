using UchebBirzha.Domain.Entities;

namespace UchebBirzha.Domain.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<IReadOnlyList<Review>> GetReviewsForExecutorAsync(string executorId);
        Task<decimal> GetAverageRatingForExecutorAsync(string executorId);
        Task<bool> HasReviewForTaskAsync(int taskId);
    }
}