using Api.Models;
using Api.Data;
using Api.DTO;
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
            .Include(p => p.Supplier)
            .Include(p => p.Categories)
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
    
    public Product Create(ProductDto productDto)
    {
        var supplier = _context.Suppliers.Find(productDto.SupplierId);
        var categories = _context.Categories.Where(c => productDto.CategoryIds.Contains(c.Id)).ToList();

        var product = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            Supplier = supplier,
            Categories = categories
        };

        _context.Products.Add(product);
        _context.SaveChanges();

        return product;
    }

    public void Update(int id, ProductDto updatedProduct)
    {
        var product = _context.Products
            .Include(p => p.Categories)
            .SingleOrDefault(p => p.Id == id);

        if (product != null)
        {
            // Update scalar properties
            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Description = updatedProduct.Description;
            product.Supplier.Id = updatedProduct.SupplierId;

            // Update categories
            if (updatedProduct.CategoryIds != null && updatedProduct.CategoryIds.Count > 0)
            {
                product.Categories.Clear();

                var categories = _context.Categories
                    .Where(c => updatedProduct.CategoryIds.Contains(c.Id))
                    .ToList();

                foreach (var category in categories)
                {
                    product.Categories.Add(category);
                }
            }

            _context.SaveChanges();
        }
    }

    public void Delete(ProductDto productToDelete)
    {
        var product = _context.Products.Find(productToDelete.Id);
        if (product is not null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}