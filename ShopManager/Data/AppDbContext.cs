using Microsoft.EntityFrameworkCore;
using ShopManager.Models;
using System.Collections.Generic;

namespace ShopManager.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Product> Products => Set<Product>();
}
