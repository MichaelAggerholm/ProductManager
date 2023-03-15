using Api.Data;
using Api.DTO;
using Api.Models;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ApiTest.ProductControllerTests;

[TestFixture]
public class GetProductByIdTest
{
    private DbContextOptions<ProductContext> _options;
    private ProductService _service;

    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<ProductContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var context = new ProductContext(_options))
        {
            context.Products.Add(new Product
                { Id = 1, Name = "Product 1", Description = "Description 1", Price = 10.0m });
            context.Products.Add(new Product
                { Id = 2, Name = "Product 2", Description = "Description 2", Price = 20.0m });
            context.SaveChanges();
        }

        var dbContext = new ProductContext(_options);
        _service = new ProductService(dbContext);
    }

    [TearDown]
    public void TearDown()
    {
        using (var context = new ProductContext(_options))
        {
            context.Database.EnsureDeleted();
        }
    }

    [Test]
    public void GetById_ReturnsCorrectProduct()
    {
        // Arrange
        var expectedProduct = new ProductDto
            { Id = 1, Name = "Product 1", Description = "Description 1", Price = 10.0m };

        // Act
        var actualProduct = _service.GetById(1);

        // Assert
        Assert.AreEqual(expectedProduct.Id, actualProduct.Id);
        Assert.AreEqual(expectedProduct.Name, actualProduct.Name);
        Assert.AreEqual(expectedProduct.Description, actualProduct.Description);
        Assert.AreEqual(expectedProduct.Price, actualProduct.Price);
    }

    [Test]
    public void GetById_ReturnsNullForNonexistentProduct()
    {
        // Arrange

        // Act
        var actualProduct = _service.GetById(999);

        // Assert
        Assert.IsNull(actualProduct);
    }
}