using Api.Controllers;
using Api.DTO;
using Api.Models;
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
        var supplier = new Supplier { Name = "Supplier1", Address = "1 test address", ContactPerson = "Supplier1", 
            PhoneNumber = "111-222-3333", Email = "Supplier1@test.com"};
        context.Suppliers.Add(supplier);
        context.SaveChanges();

        var category1 = new Category { Name = "Category 1", Description = "Category 1 description" };
        var category2 = new Category { Name = "Category 2", Description = "Category 2 description"  };
        context.Categories.AddRange(category1, category2);
        context.SaveChanges();

        var service = new ProductService(context);
        var controller = new ProductController(service);

        var categoryIds = new List<int> { 1, 2 };
        var productDto = new ProductDto { Name = "Test product", Description = "Test description", Price = 10.0m, SupplierId = 1, CategoryIds = categoryIds.ToList() };

        // Act
        var result = controller.Create(productDto) as CreatedAtActionResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result?.ActionName, Is.EqualTo(nameof(controller.GetById)));

        if (result != null)
        {
            var productToReturn = result.Value as ProductDto;
            Assert.IsNotNull(productToReturn);
            if (productToReturn != null)
            {
                Assert.That(productToReturn.Name, Is.EqualTo(productDto.Name));
                Assert.That(productToReturn.Description, Is.EqualTo(productDto.Description));
                Assert.That(productToReturn.Price, Is.EqualTo(productDto.Price));
                Assert.That(productToReturn.SupplierId, Is.EqualTo(productDto.SupplierId));
                CollectionAssert.AreEquivalent(productDto.CategoryIds, productToReturn.CategoryIds);
            }
        }
    }
}