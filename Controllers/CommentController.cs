using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApi.Dtos.Comment;
using dotnetApi.Extensions;
using dotnetApi.Interfaces;
using dotnetApi.Mappers;
using dotnetApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace dotnetApi.Controllers
{
    [Route("api/comment")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<User> _userManager;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository, UserManager<User> userManager)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _commentRepository.GetAllAsync();

            var commentsDto = comments.Select(comment => comment.ToCommentDto());

            return Ok(commentsDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }
        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockExists = await _stockRepository.StockExists(stockId);

            if (!stockExists)
            {
                return BadRequest($"Stock with id - {stockId} does not exist");
            }

            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            var commentModel = comment.ToCommentFromCreate(stockId);
            commentModel.UserId = user.Id;
            await _commentRepository.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel }, commentModel.ToCommentDto());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedComment = await _commentRepository.UpdateAsync(id, comment.ToCommentFromUpdate());

            if (updatedComment == null)
            {
                return NotFound($"Comment with id - {id} does not exist");
            }

            return Ok(comment);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepository.DeleteAsync(id);

            if (comment == null)
            {
                return NotFound($"Comment with id - {id} does not exist");
            }

            return Ok(comment);
        }
    }
}