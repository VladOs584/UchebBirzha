using System.ComponentModel.DataAnnotations;

namespace UchebBirzha.Application.DTOs.Tasks
{
    public class CreateTaskDto
    {
        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Описание обязательно")]
        [StringLength(1000, ErrorMessage = "Описание не должно превышать 1000 символов")]
        public string Description { get; set; }

        [Range(0.01, 100000, ErrorMessage = "Бюджет должен быть от 0.01 до 100000")]
        public decimal Budget { get; set; }

        [Required(ErrorMessage = "Дедлайн обязателен")]
        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = "Категория обязательна")]
        public int CategoryId { get; set; }
    }
}