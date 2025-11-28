using UchebBirzha.Application.Common;
using UchebBirzha.Domain.Enums;

namespace UchebBirzha.Application.DTOs.Users
{
    public class UserSearchDto : PagedRequestDto
    {
        public string? SearchTerm { get; set; }
        public UserRole? Role { get; set; }
        public decimal? MinRating { get; set; }
        public decimal? MaxRating { get; set; }
        public bool? IsActive { get; set; }
    }
}