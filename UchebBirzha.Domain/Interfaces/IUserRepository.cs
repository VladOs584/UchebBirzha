using UchebBirzha.Domain.Entities;

namespace UchebBirzha.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<IReadOnlyList<User>> GetExecutorsWithHighRatingAsync(decimal minRating);
        Task<bool> IsEmailUniqueAsync(string email);
    }
}