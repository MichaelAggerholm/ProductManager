using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Data;

public class CategoryContext : DbContext
{
    public CategoryContext (DbContextOptions<CategoryContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
}