using UchebBirzha.Application.DTOs.Attachments;
using UchebBirzha.Domain.Entities;

namespace UchebBirzha.Application.Mapper
{
    public static class AttachmentMappers
    {
        public static TaskAttachmentDto ToAttachmentDto(this TaskAttachment attachment)
        {
            if (attachment == null) return null;

            return new TaskAttachmentDto
            {
                Id = attachment.Id,
                FileName = attachment.FileName,
                FilePath = attachment.FilePath,
                FileSize = attachment.FileSize,
                TaskId = attachment.TaskId
            };
        }

        public static TaskAttachment ToEntity(this CreateTaskAttachmentDto dto, int taskId)
        {
            if (dto == null) return null;

            return new TaskAttachment(
                taskId: taskId,
                fileName: dto.FileName,
                filePath: dto.FilePath,
                fileSize: dto.FileSize
            );
        }
    }
}