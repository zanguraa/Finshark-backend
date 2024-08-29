namespace api.Models;

public class Comment
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Content { get; set; } = String.Empty;
    public DateTime Createdon { get; set; } = DateTime.Now;
    public int? Stockid { get; set; }
    public Stock? Stock { get; set; }
}