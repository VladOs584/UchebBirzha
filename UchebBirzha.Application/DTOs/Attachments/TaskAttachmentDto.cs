using UchebBirzha.Application.Common;

namespace UchebBirzha.Application.DTOs.Attachments
{
    public class TaskAttachmentDto : BaseDto
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public int TaskId { get; set; }
    }
}