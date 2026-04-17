using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Stocks.Models
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
