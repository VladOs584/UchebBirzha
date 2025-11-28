using UchebBirzha.Application.Common;

namespace UchebBirzha.Application.DTOs.Reviews
{
    public class ReviewDto : BaseDto
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int ExecutorId { get; set; }
        public string ExecutorName { get; set; }
    }
}