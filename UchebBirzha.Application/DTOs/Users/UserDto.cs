using UchebBirzha.Application.Common;
using UchebBirzha.Domain.Enums;

namespace UchebBirzha.Application.DTOs.Users
{
    public class UserDto : BaseDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public string? AvatarUrl { get; set; }
        public decimal? Rating { get; set; }
        public int CompletedTasksCount { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}