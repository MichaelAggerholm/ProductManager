using Api.DTO;
using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Data;

public class CategoryContext : DbContext
{
    public CategoryContext (DbContextOptions<CategoryContext> options) : base(options)
    {
    }

    public DbSet<CategoryDto> Categories => Set<CategoryDto>();
    public DbSet<ProductDto> Products => Set<ProductDto>();
}