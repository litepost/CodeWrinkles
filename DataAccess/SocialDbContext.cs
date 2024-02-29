using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class SocialDbContext(DbContextOptions options) : DbContext(options)
{
    DbSet<Post> Posts { get; set; }
}
