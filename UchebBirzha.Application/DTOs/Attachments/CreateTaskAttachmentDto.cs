using System.ComponentModel.DataAnnotations;

namespace UchebBirzha.Application.DTOs.Attachments
{
    public class CreateTaskAttachmentDto
    {
        [Required(ErrorMessage = "Имя файла обязательно")]
        public string FileName { get; set; }

        [Required(ErrorMessage = "Путь к файлу обязателен")]
        public string FilePath { get; set; }

        [Range(1, 10485760, ErrorMessage = "Размер файла должен быть от 1 байта до 10 МБ")]
        public long FileSize { get; set; }
    }
}