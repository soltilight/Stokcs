using Stocks.Models;
using StocksOperation.DTO.DTO_COMMENTS;
using System.ComponentModel.DataAnnotations;

namespace Stocks.DTO.DTO_STOCKS
{
    public class StockDto
    {

        //public int Id { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }

        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();

       
    }

    public class CreateStockRequestDto
    {
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(100,ErrorMessage ="Company Name should not exceed 100 symbols")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000000)]
        public long MarketCap { get; set; }
    }

    public class UpdateStockRequestDto
    {
       
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Company Name should not exceed 100 symbols")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000000)]
        public long MarketCap { get; set; }
    }

} 