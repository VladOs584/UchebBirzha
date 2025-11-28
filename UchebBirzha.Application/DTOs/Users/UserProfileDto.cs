using UchebBirzha.Application.Common;

namespace UchebBirzha.Application.DTOs.Users
{
    public class UserProfileDto : BaseDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? AvatarUrl { get; set; }
        public decimal? Rating { get; set; }
        public int CompletedTasksCount { get; set; }
        public int CreatedTasksCount { get; set; }
        public int ActiveBidsCount { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}