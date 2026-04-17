using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Stocks.Data;
using Stocks.DTO.DTO_STOCKS;
using Stocks.Models;
using StocksOperation.Helpers;
using StocksOperation.Interfaces;

namespace StocksOperation.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public StockRepository(ApplicationDBContext dBContext)
        {
            _dbContext=dBContext;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _dbContext.Stocks.AddAsync(stockModel);
            await _dbContext.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel= await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Id==id);
            if (stockModel == null)
            {
                return null;
            }

            _dbContext.Stocks.Remove(stockModel);
            await _dbContext.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
        {
            var stocks = _dbContext.Stocks.Include(c => c.Comments).AsQueryable();
            if (!string.IsNullOrWhiteSpace(queryObject.Industry)){
                stocks = stocks.Where(s => s.Industry.Contains(queryObject.Industry));

            }
            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if(queryObject.SortBy.Equals("Industry",StringComparison.OrdinalIgnoreCase))
                {
                    stocks = queryObject.IsDecsending ? stocks.OrderByDescending(s => s.Industry) : stocks.OrderBy(s => s.Industry);
                }
            }

            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;

            return await stocks.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
          return  await _dbContext.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i=>i.Id==id);
            
        }

        public Task<bool> StockExists(int id)
        {
            return _dbContext.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock= await _dbContext.Stocks.FirstOrDefaultAsync(x=>x.Id==id);
            if (existingStock == null)
            {
                return null;
            }
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;
            existingStock.LastDiv = stockDto.LastDiv;
            await _dbContext.SaveChangesAsync();

            return existingStock;
        }
    }
}
