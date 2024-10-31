using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetApi.Dtos.Stock
{
    public class UpdateStockDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Max length is 10")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(50, ErrorMessage = "Max length is 60")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(0.01, 10000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.091, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Max length is 50")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1, 9999999999)]
        public long MarketCap { get; set; }
    }
}