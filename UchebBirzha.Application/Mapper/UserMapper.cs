using UchebBirzha.Application.DTOs.Auth;
using UchebBirzha.Application.DTOs.Users;
using UchebBirzha.Domain.Entities;

namespace UchebBirzha.Application.Mappers
{
    public static class UserMapper
    {
        public static UserInfoDto ToUserInfoDto(this User user)
        {
            if (user == null) return null;

            return new UserInfoDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role.ToString(),
                AvatarUrl = user.AvatarUrl,
                Rating = user.Rating,
                CompletedTasksCount = user.CompletedTasksCount
            };
        }

        public static UserDto ToUserDto(this User user)
        {
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                AvatarUrl = user.AvatarUrl,
                Rating = user.Rating,
                CompletedTasksCount = user.CompletedTasksCount,
                CreatedAt = user.CreatedAt
            };
        }

        public static UserProfileDto ToUserProfileDto(this User user, int createdTasksCount, int activeBidsCount)
        {
            if (user == null) return null;

            return new UserProfileDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AvatarUrl = user.AvatarUrl,
                Rating = user.Rating,
                CompletedTasksCount = user.CompletedTasksCount,
                CreatedTasksCount = createdTasksCount,
                ActiveBidsCount = activeBidsCount
            };
        }
    }
}