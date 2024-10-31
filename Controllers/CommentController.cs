using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApi.Dtos.Comment;
using dotnetApi.Interfaces;
using dotnetApi.Mappers;
using dotnetApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnetApi.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllAsync();

            var commentsDto = comments.Select(comment => comment.ToCommentDto());

            return Ok(commentsDto);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }
        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto comment)
        {
            var stockExists = await _stockRepository.StockExists(stockId);

            if (!stockExists)
            {
                return BadRequest($"Stock with id - {stockId} does not exist");
            }

            var commentModel = comment.ToCommentFromCreate(stockId);
            await _commentRepository.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel }, commentModel.ToCommentDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto comment)
        {
            var updatedComment = await _commentRepository.UpdateAsync(id, comment.ToCommentFromUpdate());

            if (updatedComment == null)
            {
                return NotFound($"Comment with id - {id} does not exist");
            }

            return Ok(comment);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _commentRepository.DeleteAsync(id);

            if (comment == null)
            {
                return NotFound($"Comment with id - {id} does not exist");
            }

            return Ok(comment);
        }
    }
}