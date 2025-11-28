using UchebBirzha.Application.Common;
using UchebBirzha.Domain.Enums;

namespace UchebBirzha.Application.DTOs.Tasks
{
    public class TaskDetailDto : BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Budget { get; set; }
        public DateTime Deadline { get; set; }
        public TaskWorkStatus Status { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public int? ExecutorId { get; set; }
        public string? ExecutorName { get; set; }
        public string? ExecutorEmail { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int BidCount { get; set; }
        public bool IsOverdue { get; set; }
        public List<TaskAttachmentDto> Attachments { get; set; } = new();
        public List<BidDto> Bids { get; set; } = new();
    }
}