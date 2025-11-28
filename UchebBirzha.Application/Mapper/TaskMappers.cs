using UchebBirzha.Application.DTOs.Tasks;
using UchebBirzha.Application.DTOs.Attachments;
using UchebBirzha.Application.DTOs.Bids;
using UchebBirzha.Domain.Entities;
using Task = UchebBirzha.Domain.Entities.Task;

namespace UchebBirzha.Application.Mapper
{
    public static class TaskMappers
    {
        public static TaskDto ToTaskDto(this Task task)
        {
            if (task == null) return null;

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Budget = task.Budget,
                Deadline = task.Deadline,
                Status = task.Status,
                CustomerId = task.CustomerId,
                CustomerName = GetUserName(task.Customer),
                ExecutorId = task.ExecutorId,
                ExecutorName = GetUserName(task.Executor),
                CategoryId = task.CategoryId,
                CategoryName = task.Category?.Name ?? "Без категории",
                BidCount = task.Bids?.Count ?? 0,
                IsOverdue = IsTaskOverdue(task)
            };
        }

        public static TaskDetailDto ToTaskDetailDto(this Task task)
        {
            if (task == null) return null;

            return new TaskDetailDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Budget = task.Budget,
                Deadline = task.Deadline,
                Status = task.Status,
                CustomerId = task.CustomerId,
                CustomerName = GetUserName(task.Customer),
                CustomerEmail = task.Customer?.Email ?? "Email не указан",
                ExecutorId = task.ExecutorId,
                ExecutorName = GetUserName(task.Executor),
                ExecutorEmail = task.Executor?.Email,
                CategoryId = task.CategoryId,
                CategoryName = task.Category?.Name ?? "Без категории",
                BidCount = task.Bids?.Count ?? 0,
                IsOverdue = IsTaskOverdue(task),
                Attachments = task.Attachments?
                    .Select(a => a.ToAttachmentDto())
                    .ToList() ?? new List<TaskAttachmentDto>(),
                Bids = task.Bids?
                    .Select(b => b.ToBidDto())
                    .ToList() ?? new List<BidDto>()
            };
        }

        private static string GetUserName(User user)
        {
            return user != null ? $"{user.FirstName} {user.LastName}" : null;
        }

        private static bool IsTaskOverdue(Task task)
        {
            return task.Deadline < DateTime.UtcNow &&
                   task.Status != Domain.Enums.TaskWorkStatus.Completed &&
                   task.Status != Domain.Enums.TaskWorkStatus.Cancelled;
        }
    }
}