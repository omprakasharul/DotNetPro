using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {

        }

        public DbSet<Movie> MoviesList {get; set;} = null!;

    }
}
