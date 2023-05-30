using Microsoft.EntityFrameworkCore;

namespace BooksSample.Data;

public class BooksContext : DbContext
{
    public BooksContext(DbContextOptions<BooksContext> options)
        : base(options)
    {            
    }

    public DbSet<Book> Books => Set<Book>();
}
