using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Data;

public class SupplierContext : DbContext
{
    public SupplierContext (DbContextOptions<SupplierContext> options) : base(options)
    {
    }

    public DbSet<Supplier> Suppliers => Set<Supplier>();
}