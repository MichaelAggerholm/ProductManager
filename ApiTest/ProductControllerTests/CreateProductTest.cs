/*using Api.Controllers;
using Api.DTO;
using Api.Services;
using Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.ProductControllerTests;

[TestFixture]
public class CreateProductTest
{
    [Test]
    public void Create_ReturnsCreatedAtActionResult_WithCreatedProduct()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ProductContext>()
            .UseInMemoryDatabase(databaseName: "ProductManager")
            .Options;

        var context = new ProductContext(options);
        var supplier = new SupplierDto("Supplier1", "1 test address", "Supplier1", "111-222-3333", "Supplier1@test.com");
        context.Suppliers.Add(supplier);
        context.SaveChanges();

        var category1 = new CategoryDto("Category 1", "Category 1 description");
        var category2 = new CategoryDto("Category 2", "Category 2 description");
        
        category1.Products.Add(new ProductDto("Test product", "Test description", 10.0m, supplier, new List<CategoryDto> { category1, category2 }));
        
        context.Categories.AddRange(category1, category2);
        context.SaveChanges();

        var service = new ProductService(context);
        var controller = new ProductController(service);

        var productDto = new ProductDto("Test product", "Test description", 10.0m, supplier, new List<CategoryDto> { category1, category2 });

        // Act
        var result = controller.Create(productDto) as CreatedAtActionResult;

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result?.ActionName, Is.EqualTo(nameof(controller.GetById)));

        if (result == null) return;
        var productToReturn = result.Value as ProductDto;
        Assert.That(productToReturn, Is.Not.Null);
        if (productToReturn == null) return;
        Assert.Multiple(() =>
        {
            Assert.That(productToReturn.Name, Is.EqualTo(productDto.Name));
            Assert.That(productToReturn.Description, Is.EqualTo(productDto.Description));
            Assert.That(productToReturn.Price, Is.EqualTo(productDto.Price));
            Assert.That(productToReturn.Supplier, Is.EqualTo(productDto.Supplier));
        });
        CollectionAssert.AreEquivalent(productDto.Categories, productToReturn.Categories);
    }
}*/