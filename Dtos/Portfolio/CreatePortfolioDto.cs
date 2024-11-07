using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetApi.Dtos.Portfolio
{
    public class CreatePortfolioDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Min length is 1")]
        public string Symbol { get; set; } = string.Empty;
    }
}