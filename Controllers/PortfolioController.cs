using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using dotnetApi.Dtos.Portfolio;
using dotnetApi.Extensions;
using dotnetApi.Interfaces;
using dotnetApi.Models;
using dotnetApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnetApi.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    [Authorize]

    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        public PortfolioController(UserManager<User> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            Console.WriteLine(JsonSerializer.Serialize(user, new JsonSerializerOptions { WriteIndented = true }));

            if (user == null)
                return BadRequest();

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(user);

            Console.WriteLine(JsonSerializer.Serialize(userPortfolio, new JsonSerializerOptions { WriteIndented = true }));
            return Ok(userPortfolio);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserPortfolio([FromBody] CreatePortfolioDto createPortfolioDto)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepository.GetBySymbolAsync(createPortfolioDto.Symbol);
            if (stock == null)
                return BadRequest("Stock not found");

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(user);
            if (userPortfolio.Any(s => s.Symbol.ToLower() == createPortfolioDto.Symbol.ToLower()))
                return BadRequest("Already exist in portfolio");

            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                UserId = user.Id,
            };

            await _portfolioRepository.CreateAsync(portfolioModel);

            if (portfolioModel == null)
                return StatusCode(500, "Could not create");

            return Created();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeletePortfolioDto deletePortfolioDto)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var portfolio = await _portfolioRepository.Delete(user, deletePortfolioDto.Symbol);

            if (portfolio == null)
                return BadRequest();

            return Ok();
        }
    }
}