using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class StockRepository : IStockRepository
{
    
    private readonly ApplicationDBContext _context;
    
    public StockRepository(ApplicationDBContext dbContext)
    {
        _context = dbContext;
    }
    public async Task<List<Stock>> GetAllAsync()
    {
        return await _context.Stocks.ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _context.Stocks.FindAsync(id);
    }

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
        await _context.Stocks.AddAsync(stockModel);
        await _context.SaveChangesAsync();
        return stockModel;
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockRequestDto)
    {
        var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

        if (existingStock == null)
        {
            return null;
        }
        
        existingStock.Symbol = stockRequestDto.Symbol;
        existingStock.Companyname = stockRequestDto.Companyname;
        existingStock.Purchase = stockRequestDto.Purchase;
        existingStock.LastDiv = stockRequestDto.LastDiv;
        existingStock.Industry = stockRequestDto.Industry;
        existingStock.MarketCap = stockRequestDto.MarketCap;

        await _context.SaveChangesAsync();
        return existingStock;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stockModel == null)
        {
            return null;
        }

        _context.Stocks.Remove(stockModel);
        await _context.SaveChangesAsync();
        return  stockModel;
    }
}