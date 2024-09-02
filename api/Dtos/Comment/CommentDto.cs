using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment;

public class CommentDto
{
    public int Id { get; set; }
    [Required]
    [MinLength(5, ErrorMessage = "Title must be minimum 5 char")]
    [MaxLength(280, ErrorMessage = "Title can not be over 200 char")]
    public string Title { get; set; } = String.Empty;
    [Required]
    [MinLength(5, ErrorMessage = "Content must be minimum 5 char")]
    [MaxLength(280, ErrorMessage = "Content can not be over 200 char")]
    public string Content { get; set; } = String.Empty;
    public DateTime Createdon { get; set; } = DateTime.Now;
    public int? Stockid { get; set; }
}