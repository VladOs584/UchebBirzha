using UchebBirzha.Application.DTOs.Attachments;

namespace UchebBirzha.Application.Interfaces
{
    public interface IAttachmentService
    {
        Task<TaskAttachmentDto> GetAttachmentByIdAsync(int id);
        Task<IReadOnlyList<TaskAttachmentDto>> GetAttachmentsForTaskAsync(int taskId);
        Task<TaskAttachmentDto> UploadAttachmentAsync(CreateTaskAttachmentDto createDto, int taskId, int userId);
        Task DeleteAttachmentAsync(int attachmentId, int userId);
    }
}