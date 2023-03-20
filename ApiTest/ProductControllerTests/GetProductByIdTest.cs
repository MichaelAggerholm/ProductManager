/*using Api.Data;
using Api.DTO;
using Api.Services;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.ProductControllerTests;

[TestFixture]
public class GetProductByIdTest
{
    private readonly DbContextOptions<ProductContext> _options;
    private readonly ProductService _service;

    public GetProductByIdTest()
    {
        _options = new DbContextOptionsBuilder<ProductContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var context = new ProductContext(_options))
        {
            context.Products.Add(new ProductDto("Product 1", "Description 1", 10.0m,
                new SupplierDto("Supplier 1", "Address 1", "Contact 1", "555-555-5555", "Email 1"),
                new List<CategoryDto> { new CategoryDto("Category 1", "Description 1") }));
            context.Products.Add(new ProductDto("Product 2", "Description 2", 20.0m,
                new SupplierDto("Supplier 2", "Address 2", "Contact 2", "555-555-5555", "Email 2"),
                new List<CategoryDto> { new CategoryDto("Category 2", "Description 2") }));
            context.SaveChanges();
        }

        var dbContext = new ProductContext(_options);
        _service = new ProductService(dbContext);
    }

    [TearDown]
    public void TearDown()
    {
        using var context = new ProductContext(_options);
        context.Database.EnsureDeleted();
    }

    [Test]
    public void GetById_ReturnsCorrectProduct()
    {
        // Arrange
        var expectedProduct = new ProductDto("Product 1", "Description 1", 10.0m,
            new SupplierDto("Supplier 1", "Address 1", "Contact 1", "555-555-5555", "Email 1"),
            new List<CategoryDto> { new CategoryDto("Category 1", "Description 1") });

        // Act
        var actualProduct = _service.GetById(1);

        // Assert
        if (actualProduct == null) return;
        Assert.Multiple(() =>
        {
            Assert.That(actualProduct.Id, Is.EqualTo(expectedProduct.Id));
            Assert.That(actualProduct.Name, Is.EqualTo(expectedProduct.Name));
            Assert.That(actualProduct.Description, Is.EqualTo(expectedProduct.Description));
            Assert.That(actualProduct.Price, Is.EqualTo(expectedProduct.Price));
        });
    }

    [Test]
    public void GetById_ReturnsNullForNonexistentProduct()
    {
        // Arrange

        // Act
        var actualProduct = _service.GetById(999);

        // Assert
        Assert.That(actualProduct, Is.Null);
    }
}*/