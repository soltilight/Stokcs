using System.ComponentModel.DataAnnotations;

namespace Stocks.Models
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(10)]
        public string Symbol { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(20)]
        public string CompanyName { get; set; } = string.Empty;
        
        [Required]
        [Range(0, 1000000000)]
        public decimal Purchase { get; set; }
        
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Industry { get; set; } = string.Empty;
        
        [Required]
        [Range(1, 10000000000)]
        public long MarketCap { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
