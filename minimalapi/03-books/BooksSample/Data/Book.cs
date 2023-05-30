namespace BooksSample.Data;

public class Book
{
    public int BookId { get; set; }
    public required string Title { get; set; }
    public string? Publisher { get; set; }
}
