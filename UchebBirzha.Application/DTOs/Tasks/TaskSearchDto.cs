using UchebBirzha.Domain.Enums;

namespace UchebBirzha.Application.DTOs.Tasks
{
    public class TaskSearchDto : PagedRequestDto
    {
        public string? SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public decimal? MinBudget { get; set; }
        public decimal? MaxBudget { get; set; }
        public TaskWorkStatus? Status { get; set; }
        public bool? OnlyWithBids { get; set; }
        public string? SortBy { get; set; } 
    }
}