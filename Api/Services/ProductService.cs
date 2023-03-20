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

    public ProductDto MapToDto(ProductDto product)
    {
        var categories = product.Categories.Select(category => new CategoryDto(category.Name, category.Description)
            { Id = category.Id }).ToList();

        return new ProductDto(product.Name, product.Description, product.Price)
        {
            Supplier = new SupplierDto(product.Supplier.Name, product.Supplier.Address,
                product.Supplier.ContactPerson,
                product.Supplier.PhoneNumber, product.Supplier.Email),
            Categories = categories
        };
    }

    public IEnumerable<ProductDto> GetAll()
    {
        var products = _context.Products
            .Include(p => p.Supplier)
            .Include(p => p.Categories)
            .AsNoTracking()
            .ToList();

        return products.Select(MapToDto).ToList();
    }

    public ProductDto GetById(int id)
    {
        return _context.Products
            .Include(p => p.Categories)
            .Include(p => p.Supplier)
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id) ?? throw new InvalidOperationException();
    }

    public ProductDto Create(ProductDto productDto)
    {
        var product = new ProductDto(productDto.Name, productDto.Description, productDto.Price);

        // set supplier

        if (true)
        {
            if (productDto.Supplier != null)
            {
                var supplier = _context.Suppliers.Find(productDto.Supplier.Id);
                if (supplier != null) product.Supplier = supplier;
            }
        }

        // set categories
        if (true)
        {
            var categoryIds = productDto.Categories.Select(c => c.Id).ToList();
            var categories = _context.Categories
                .AsEnumerable()
                .Where(c => categoryIds.Contains(c.Id))
                .ToList();

            product.Categories = categories;
        }

        _context.Products.Add(product);
        _context.SaveChanges();

        productDto.Id = product.Id;

        return productDto;
    }

    public void Update(int id, ProductDto updatedProduct)
    {
        var product = _context.Products
            .Include(p => p.Categories)
            .SingleOrDefault(p => p.Id == id);

        if (product != null)
        {
            if (true){
                if (updatedProduct.Supplier != null)
                {
                    var supplier = _context.Suppliers.Find(updatedProduct.Supplier.Id);
                    if (true)
                    {
                        var categoryIds = updatedProduct.Categories.Select(c => c.Id).ToList();
                        var categories = _context.Categories
                            .AsEnumerable()
                            .Where(c => categoryIds.Contains(c.Id))
                            .ToList();

                        // Update scalar properties
                        product.Name = updatedProduct.Name;
                        product.Price = updatedProduct.Price;
                        product.Description = updatedProduct.Description;
                        // HUSK, lav et tjek på null værdier, supplier bør ikke kunne tilføjes hvis null.
                        product.Supplier = supplier;
                        product.Categories = categories;
                    }
                }
            }

            _context.SaveChanges();
        }
    }

    public void Delete(ProductDto? productToDelete)
    {
        if (productToDelete == null) return;
        var product = _context.Products.Find(productToDelete.Id);
        if (product is null) return;
        _context.Products.Remove(product);
        _context.SaveChanges();
    }
}