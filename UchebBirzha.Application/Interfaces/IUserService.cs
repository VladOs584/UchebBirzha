using UchebBirzha.Application.DTOs.Users;

namespace UchebBirzha.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<UserProfileDto> GetUserProfileAsync(int id);
        Task<IReadOnlyList<UserDto>> GetTopExecutorsAsync(int count = 10);
        Task<UserDto> UpdateUserProfileAsync(int userId, UpdateUserDto updateUserDto);
        Task UpdateUserRatingAsync(int executorId, decimal newRating);
    }
}