using Api.Controllers;
using Api.DTO;
using Api.Models;
using Api.Services;
using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.ProductControllerTests
{
    [TestFixture]
    public class GetAllProductsTest
    {
        [Test]
        public void GetAll_ReturnsListOfProducts()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase(databaseName: "ProductManager")
                .Options;

            var context = new ProductContext(options);
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Screws", Description = "Screws for construction", Price = 10.00m},
                new Product { Id = 2, Name = "Flat iron", Description = "Flat iron for cooking", Price = 20.00m },
                new Product { Id = 3, Name = "Ladder", Description = "Ladder for construction", Price = 30.00m }
            };

            context.Products.AddRange(products);
            context.SaveChanges();

            var service = new ProductService(context);
            var controller = new ProductController(service);

            // Act
            var result = controller.GetAll();

            // Assert
            Assert.IsInstanceOf<IEnumerable<ProductDto>>(result);
        }
    }
}