using Stocks.DTO.DTO_STOCKS;
using Stocks.Models;
using StocksOperation.Mappers;

namespace Stocks.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                LastDiv = stockModel.LastDiv,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

       

     public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
                LastDiv = stockDto.LastDiv,
                
            };
        }
    }

}

