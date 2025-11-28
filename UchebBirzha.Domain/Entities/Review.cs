using UchebBirzha.Domain.Common;

namespace UchebBirzha.Domain.Entities
{
    public class Review : BaseEntity
    {
        public int Rating { get; private set; } 
        public string Comment { get; private set; }
        public DateTime CreatedAt { get; private set; }

       
        public int TaskId { get; private set; }
        public int AuthorId { get; private set; } // Кто оставил отзыв (заказчик)
        public int ExecutorId { get; private set; } // Кому отзыв (исполнитель)

       
        public virtual Task Task { get; private set; }
        public virtual User Author { get; private set; }
        public virtual User Executor { get; private set; }

        private Review() { }

        public Review(int taskId, int authorId, int executorId, int rating, string comment)
        {
            TaskId = taskId;
            AuthorId = authorId;
            ExecutorId = executorId;
            Rating = rating;
            Comment = comment;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateReview(int rating, string comment)
        {
            Rating = rating;
            Comment = comment;
        }
    }
}