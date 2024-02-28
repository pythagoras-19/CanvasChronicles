using CanvasChronicles.Models;
using Microsoft.EntityFrameworkCore;

namespace CanvasChronicles.Data; 

public class BloggingContext : DbContext 
{
    public BloggingContext(DbContextOptions<BloggingContext> options)
        : base(options)
    { }

    public DbSet<BlogPost> BlogPosts { get; set; }
}