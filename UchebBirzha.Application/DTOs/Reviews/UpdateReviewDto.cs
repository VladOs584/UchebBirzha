using System.ComponentModel.DataAnnotations;

namespace UchebBirzha.Application.DTOs.Reviews
{
    public class UpdateReviewDto
    {
        [Required(ErrorMessage = "Рейтинг обязателен")]
        [Range(1, 5, ErrorMessage = "Рейтинг должен быть от 1 до 5")]
        public int Rating { get; set; }

        [StringLength(1000, ErrorMessage = "Комментарий не должен превышать 1000 символов")]
        public string Comment { get; set; }
    }
}