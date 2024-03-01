using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataAccess;

public class SocialDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Post> Posts { get; set; }
}
