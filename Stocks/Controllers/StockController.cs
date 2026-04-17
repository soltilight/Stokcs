using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stocks.Data;
using Stocks.DTO.DTO_STOCKS;
using Stocks.Mappers;
using StocksOperation.Helpers;
using StocksOperation.Interfaces;
using StocksOperation.Repository;
namespace Stocks.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBContext context,IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
            _dbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var context = await _stockRepo.GetAllAsync(query);
            var stockDto= context.Select(s => s.ToStockDto());
            return Ok(stockDto);
        }
        
        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public async Task <IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id },
                                   stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        
        public async Task <IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = await _stockRepo.UpdateAsync( id, updateDto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (stockModel == null)
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]

        public async Task <IActionResult> Delete([FromRoute] int id) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _stockRepo.DeleteAsync(id);

            if(stockModel==null)
            {
                return NotFound();
            }


            return NoContent();
        }
        

    }
}
