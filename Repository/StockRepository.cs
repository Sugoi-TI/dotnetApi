using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApi.Data;
using dotnetApi.Dtos.Stock;
using dotnetApi.Interfaces;
using dotnetApi.Mappers;
using dotnetApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace dotnetApi.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.Include(s => s.Comments).ToListAsync();
        }
        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stock = await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);

            return stock;
        }
        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();

            return stockModel;
        }
        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

            if (stockModel == null)
            {
                return null;
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }
        public async Task<Stock?> UpdateAsync(int id, UpdateStockDto updateStockDto)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

            if (stockModel == null)
            {
                return null;
            }

            stockModel.Symbol = updateStockDto.Symbol;
            stockModel.CompanyName = updateStockDto.CompanyName;
            stockModel.Purchase = updateStockDto.Purchase;
            stockModel.LastDiv = updateStockDto.LastDiv;
            stockModel.Industry = updateStockDto.Industry;
            stockModel.marketCap = updateStockDto.marketCap;

            await _context.SaveChangesAsync();

            return stockModel;
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stocks.AnyAsync(s => s.Id == id);
        }
    }
}