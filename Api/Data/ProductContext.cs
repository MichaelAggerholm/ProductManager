using Api.DTO;
using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Data;

public class ProductContext : DbContext
{
    public ProductContext (DbContextOptions<ProductContext> options) : base(options)
    {
    }

    public DbSet<ProductDto> Products => Set<ProductDto>();
    public DbSet<CategoryDto> Categories => Set<CategoryDto>();
    public DbSet<SupplierDto> Suppliers => Set<SupplierDto>();
}