using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApi.Dtos.Comment;
using dotnetApi.Models;

namespace dotnetApi.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreateOn = commentModel.CreateOn,
                StockId = commentModel.StockId
            };
        }
        public static Comment ToCommentFromCreate(this CreateCommentDto comment, int stockId)
        {
            return new Comment
            {
                Title = comment.Title,
                Content = comment.Content,
                StockId = stockId,
            };
        }
    }
}