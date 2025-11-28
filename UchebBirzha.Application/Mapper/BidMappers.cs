using UchebBirzha.Application.DTOs.Bids;
using UchebBirzha.Domain.Entities;

namespace UchebBirzha.Application.Mapper
{
    public static class BidMappers
    {
        public static BidDto ToBidDto(this Bid bid)
        {
            if (bid == null) return null;

            return new BidDto
            {
                Id = bid.Id,
                ProposedPrice = bid.ProposedPrice,
                Comment = bid.Comment,
                CreatedAt = bid.CreatedAt,
                IsAccepted = bid.IsAccepted,
                TaskId = bid.TaskId,
                ExecutorId = bid.ExecutorId,
                ExecutorName = bid.Executor != null ? $"{bid.Executor.FirstName} {bid.Executor.LastName}" : "Неизвестный исполнитель",
                ExecutorRating = bid.Executor?.Rating
            };
        }
    }
}