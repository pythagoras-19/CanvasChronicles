using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalBlog2.Models; 

namespace PersonalBlog2.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUserModel> 
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}