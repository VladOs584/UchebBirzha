using System.Security.Cryptography;
using UchebBirzha.Domain.Common;
using UchebBirzha.Domain.Enums;

namespace UchebBirzha.Domain.Entities
{
    public class Task : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Budget { get; private set; }
        public DateTime Deadline { get; private set; }
        public TaskWorkStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        
        public int CustomerId { get; private set; }
        public int CategoryId { get; private set; }
        public int? ExecutorId { get; private set; }

     
        public virtual User Customer { get; private set; }
        public virtual User? Executor { get; private set; }
        public virtual Category Category { get; private set; }
        public virtual ICollection<Bid> Bids { get; private set; } = new List<Bid>();
        public virtual ICollection<TaskAttachment> Attachments { get; private set; } = new List<TaskAttachment>();

        private Task() { }

        public Task(string title, string description, decimal budget, DateTime deadline, int customerId, int categoryId)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Budget = budget > 0 ? budget : throw new ArgumentException("Budget must be positive");
            Deadline = deadline > DateTime.UtcNow ? deadline : throw new ArgumentException("Deadline must be in future");
            CustomerId = customerId;
            CategoryId = categoryId;

            Status = TaskWorkStatus.Open;
            CreatedAt = DateTime.UtcNow; 
        }


        public void ChangeStatus(TaskWorkStatus newStatus)
        {
            Status = newStatus;
        }

        public void SetExecutor(int executorId)
        {
            ExecutorId = executorId;
        }

        public void UpdateDetails(string title, string description, decimal budget, DateTime deadline)
        {
            Title = title;
            Description = description;
            Budget = budget;
            Deadline = deadline;
        }
    }
}