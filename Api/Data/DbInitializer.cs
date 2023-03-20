using Api.DTO;
using Api.Models;

namespace Api.Data;

public static class DbInitializer
{
    public static void Initialize(ProductContext context)
    {
        if (context.Products.Any() && context.Categories.Any() && context.Suppliers.Any())
        {
            return; // DB has been seeded
        }
        
        var beverages = new CategoryDto("Beverages", "Soft drinks, coffees, teas, beers, and ales");
        var condiments = new CategoryDto("Condiments", "Sweet and savory sauces, relishes, spreads, and seasonings");
        var dairyProducts = new CategoryDto("Dairy Products", "Cheeses");
        var grainsCereals = new CategoryDto("Grains/Cereals", "Breads, crackers, pasta, and cereal");

        var jefa = new SupplierDto("Jefa Foods", "1 Jefa Way", "Jefa", "555-555-5555", "Jefa@gmail.com");
        var stary = new SupplierDto("Stary Foods", "1 Stary Way", "Stary", "555-555-5555", "Stary@gmail.com");

        var productOne = new ProductDto("Chai", "Spicy chai tea", 18.00M)
        {
            Supplier = jefa,
            Categories = new List<CategoryDto> { beverages, condiments }
        };

        var productTwo = new ProductDto("Chang", "An alcoholic beverage", 19.00M)
        {
            Supplier = stary,
            Categories = new List<CategoryDto> { condiments }
        };

        var productThree = new ProductDto("Aniseed Syrup", "Sweet syrup", 10.00M)
        {
            Supplier = jefa,
            Categories = new List<CategoryDto> { grainsCereals }
        };

        var productFour = new ProductDto("Cheddar", "Aged cheddar cheese", 10.00M)
        {
            Supplier = stary,
            Categories = new List<CategoryDto> { beverages, condiments }
        };

        var productFive = new ProductDto("Gnocchi di nonna Alice", "Gnocchi with nonna's sauce", 38.00M)
        {
            Supplier = stary,
            Categories = new List<CategoryDto> { dairyProducts, condiments }
        };

        beverages.Products.AddRange(new List<ProductDto> { productOne, productThree });
        condiments.Products.AddRange(new List<ProductDto> { productOne, productThree });
        dairyProducts.Products.Add(productFour);
        grainsCereals.Products.AddRange(new List<ProductDto> { productFour, productFive });

        context.Products.AddRange(productOne, productTwo, productThree, productFour, productFive);
        context.SaveChanges();
    }
}