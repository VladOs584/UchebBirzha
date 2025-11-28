using System.ComponentModel.DataAnnotations;

namespace UchebBirzha.Application.DTOs.Bids
{
    public class UpdateBidDto
    {
        [Required(ErrorMessage = "Предлагаемая цена обязательна")]
        [Range(0.01, 100000, ErrorMessage = "Цена должна быть от 0.01 до 100000")]
        public decimal ProposedPrice { get; set; }

        [StringLength(500, ErrorMessage = "Комментарий не должен превышать 500 символов")]
        public string Comment { get; set; }
    }
}