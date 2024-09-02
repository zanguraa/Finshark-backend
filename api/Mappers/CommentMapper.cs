using api.Dtos.Comment;
using api.Models;

namespace api.Mappers;

public static class CommentMapper
{
    public static CommentDto ToCommentDto(this Comment commentModel)
    {
        return new CommentDto
        {
            Id = commentModel.Id,
            Title = commentModel.Title,
            Content = commentModel.Content,
            Createdon = commentModel.Createdon,
            Stockid = commentModel.Stockid,
        };
    }

    public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int stockId)
    {
        return new Comment
        {
            Title = commentDto.Title,
            Content = commentDto.Content,
            Stockid = stockId
        };
    }

    public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto commentDto, int stockId)
    {
        return new Comment
        {
            Title = commentDto.Title,
            Content = commentDto.Content,
            Stockid = stockId
        };
    }
}