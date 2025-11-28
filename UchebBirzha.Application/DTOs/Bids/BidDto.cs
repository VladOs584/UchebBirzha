using UchebBirzha.Application.Common;

namespace UchebBirzha.Application.DTOs.Bids
{
    public class BidDto : BaseDto
    {
        public decimal ProposedPrice { get; set; }
        public string Comment { get; set; }
        public bool IsAccepted { get; set; }
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public int ExecutorId { get; set; }
        public string ExecutorName { get; set; }
        public decimal? ExecutorRating { get; set; }
        public int ExecutorCompletedTasks { get; set; }
    }
}