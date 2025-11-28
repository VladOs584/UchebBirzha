using UchebBirzha.Application.DTOs.Reviews;

namespace UchebBirzha.Application.Interfaces
{
    public interface IReviewService
    {
       
        Task<ReviewDto> GetReviewByIdAsync(int id);
        Task<IReadOnlyList<ReviewDto>> GetReviewsForExecutorAsync(int executorId);
        Task<ReviewDto> GetReviewForTaskAsync(int taskId);

        
        Task<ReviewDto> CreateReviewAsync(CreateReviewDto createReviewDto, int authorId);
        Task<ReviewDto> UpdateReviewAsync(int reviewId, UpdateReviewDto updateReviewDto, int authorId);
        Task DeleteReviewAsync(int reviewId, int authorId);

        Task<decimal> GetAverageRatingForExecutorAsync(int executorId);
    }
}