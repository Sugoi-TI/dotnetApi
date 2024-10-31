using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetApi.Dtos.Comment
{
    public class UpdateCommentDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Min length is 3")]
        [MaxLength(60, ErrorMessage = "Man length is 280")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(3, ErrorMessage = "Min length is 3")]
        [MaxLength(60, ErrorMessage = "Man length is 280")]
        public string Content { get; set; } = string.Empty;
    }
}