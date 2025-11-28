using UchebBirzha.Domain.Common;

namespace UchebBirzha.Domain.Entities
{
    public class Bid : BaseEntity
    {
        public decimal ProposedPrice { get; private set; }
        public string Comment { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsAccepted { get; private set; }

      
        public int TaskId { get; private set; }
        public int ExecutorId { get; private set; }

        public virtual Task Task { get; private set; }
        public virtual User Executor { get; private set; }

        private Bid() { }
        
        public Bid(int taskId, int executorId, decimal proposedPrice, string comment)
        {
            TaskId = taskId;
            ExecutorId = executorId;
            ProposedPrice = proposedPrice;
            Comment = comment;
            CreatedAt = DateTime.UtcNow;
            IsAccepted = false;
        }

        public void Accept()
        {
            IsAccepted = true;
        }

        public void UpdateProposedPrice(decimal newPrice)
        {
            ProposedPrice = newPrice;
        }
    }
}