using System.ComponentModel.DataAnnotations;

namespace UchebBirzha.Application.DTOs.Users
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "Имя обязательно")]
        [StringLength(50, ErrorMessage = "Имя не должно превышать 50 символов")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна")]
        [StringLength(50, ErrorMessage = "Фамилия не должна превышать 50 символов")]
        public string LastName { get; set; }

        [Url(ErrorMessage = "Некорректный URL аватара")]
        public string? AvatarUrl { get; set; }
    }
}