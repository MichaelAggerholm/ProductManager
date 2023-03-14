using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Data;

public class ProductContext : DbContext
{
    public ProductContext (DbContextOptions<ProductContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
}