using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApi.Models;

namespace dotnetApi.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(User user);
        Task<Portfolio> CreateAsync(Portfolio portfolio);
        Task<Portfolio?> Delete(User user, string symbol);
    }
}