using Api.Models;
using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class ProductService
{
    private readonly ProductContext _context;
    
    public ProductService(ProductContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Product> GetAll()
    {
        return _context.Products
            .AsNoTracking()
            .ToList();
    }
    
    public Product? GetById(int id)
    {
        return _context.Products
            .Include(p => p.Categories)
            .Include(p => p.Supplier)
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);
    }
    
    public Product? Create(Product newProduct)
    {
        _context.Products.Add(newProduct);
        _context.SaveChanges();
        
        return newProduct;
    }

    public void AddCategory(int ProductId, int CategoryId)
    {
        var ProductToUpdate = _context.Products.Find(ProductId);
        var CategoryToAdd = _context.Categories.Find(CategoryId);
        
        if (ProductToUpdate is null || CategoryToAdd is null)
        {
            throw new Exception("Product or Category not found");
        }

        if (ProductToUpdate.Categories is null)
        {
            ProductToUpdate.Categories = new List<Category>();
        }
        
        ProductToUpdate.Categories.Add(CategoryToAdd);
        
        _context.SaveChanges();
    }
    
    public void UpdateSupplier(int ProductId, int SupplierId)
    {
        var ProductToUpdate = _context.Products.Find(ProductId);
        var SupplierToAdd = _context.Suppliers.Find(SupplierId);
        
        if (ProductToUpdate is null || SupplierToAdd is null)
        {
            throw new Exception("Product or Supplier not found");
        }

        ProductToUpdate.Supplier = SupplierToAdd;
        
        _context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var ProductToDelete = _context.Products.Find(id);
        if (ProductToDelete is not null)
        {
            _context.Products.Remove(ProductToDelete);
            _context.SaveChanges();
        }
    }
}