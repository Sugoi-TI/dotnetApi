using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApi.Dtos.Stock;
using dotnetApi.Helpers;
using dotnetApi.Models;

namespace dotnetApi.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockDto updateStockDto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}