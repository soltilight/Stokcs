using Stocks.DTO.DTO_STOCKS;
using Stocks.Models;
using StocksOperation.Helpers;

namespace StocksOperation.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject queryObject);
        
        Task<Stock?> GetByIdAsync(int id);
       
        Task<Stock> CreateAsync(Stock stockModel);

        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockModel);

        Task<Stock?> DeleteAsync(int id);

        Task<bool> StockExists(int id);
    }
}
