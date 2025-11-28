using UchebBirzha.Domain.Common;

namespace UchebBirzha.Domain.Entities
{
    public class TaskAttachment : BaseEntity
    {
        public string FileName { get; private set; }
        public string FilePath { get; private set; }
        public long FileSize { get; private set; }
        public DateTime UploadedAt { get; private set; }

        
        public int TaskId { get; private set; }

        
        public virtual Task Task { get; private set; }

        private TaskAttachment() { }

        public TaskAttachment(int taskId, string fileName, string filePath, long fileSize)
        {
            TaskId = taskId;
            FileName = fileName;
            FilePath = filePath;
            FileSize = fileSize;
            UploadedAt = DateTime.UtcNow;
        }
    }
}