using Microsoft.EntityFrameworkCore;
using PocketBook.Models;

namespace PocketBook.Data;

public class AppDbContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}