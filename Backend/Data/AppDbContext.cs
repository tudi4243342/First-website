using Microsoft.EntityFrameworkCore;
using First_website.Backend.Models;
using First_website.Models;

namespace First_website.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // This represents the Fruits table in the database map
    public DbSet<User> Users => Set<User>();
}