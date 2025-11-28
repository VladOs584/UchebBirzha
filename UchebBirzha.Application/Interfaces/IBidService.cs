using UchebBirzha.Application.DTOs.Bids;

namespace UchebBirzha.Application.Interfaces
{
    public interface IBidService
    {
       
        Task<BidDto> GetBidByIdAsync(int id);
        Task<IReadOnlyList<BidDto>> GetBidsForTaskAsync(int taskId);
        Task<IReadOnlyList<BidDto>> GetBidsByExecutorAsync(int executorId);

     
        Task<BidDto> PlaceBidAsync(CreateBidDto createBidDto, int executorId);
        Task<BidDto> UpdateBidAsync(int bidId, UpdateBidDto updateBidDto, int executorId);
        Task DeleteBidAsync(int bidId, int executorId);


        Task AcceptBidAsync(int bidId, int customerId);
        Task RejectBidAsync(int bidId, int customerId);
    }
}