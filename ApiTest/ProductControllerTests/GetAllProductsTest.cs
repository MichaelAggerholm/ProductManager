/*using Api.Controllers;
using Api.DTO;
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
            var products = new List<ProductDto>
            {
                new ProductDto("Screws", "Screws for construction", 10.00m, new SupplierDto("Dave's Hardware", "123 Main St", "Dave", "555-555-5555", "Dave@gmail.com"), new List<CategoryDto> { new CategoryDto("Hardware", "Hardware for construction") }),
                new ProductDto("Nails", "Nails for construction", 10.00m, new SupplierDto("Dave's Hardware", "123 Main St", "Dave", "555-555-5555", "Dave@gmail.com"), new List<CategoryDto> { new CategoryDto("Hardware", "Hardware for construction") }),
                new ProductDto("Hammer", "Hammer for construction", 10.00m, new SupplierDto("Dave's Hardware", "123 Main St", "Dave", "555-555-5555", "Dave@gmail.com"), new List<CategoryDto> { new CategoryDto("Hardware", "Hardware for construction") }),
            };

            context.Products.AddRange(products);
            context.SaveChanges();

            var service = new ProductService(context);
            var controller = new ProductController(service);

            // Act
            var result = controller.GetAll();

            // Assert
            Assert.That(result, Is.InstanceOf<IEnumerable<ProductDto>>());
        }
    }
}*/