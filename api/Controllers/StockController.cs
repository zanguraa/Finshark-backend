using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    private readonly IStockRepository _stockRepository;
    public StockController(ApplicationDBContext applicationDbContext, IStockRepository stockRepository)
    {
        _context = applicationDbContext;
        _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<IActionResult>  GetAll([FromQuery] QueryObject query)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var stocks = await _stockRepository.GetAllAsync(query);
        var stockDto = stocks.Select(s => s.ToStockDto());
        return Ok(stocks);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var stock = await _stockRepository.GetByIdAsync(id);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var stockModel =  stockDto.ToStockFromCreateDTO();
        await _stockRepository.CreateAsync(stockModel);
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id}, stockModel.ToStockDto() );
    }

    [HttpPut]
    [Route(("{id:int}"))]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var stockModel = await _stockRepository.UpdateAsync(id, updateDto);

        if (stockModel == null)
        {
            return NotFound();
        } 
        await _context.SaveChangesAsync();
        
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var stockModel = await _stockRepository.DeleteAsync(id);

        if (stockModel == null)
        {
            return NotFound();
        }
        return NoContent();
    }
    
}