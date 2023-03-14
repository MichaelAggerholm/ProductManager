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
        
        var beverages = new Category { Name = "Beverages" };
        var condiments = new Category { Name = "Condiments", Description = "Sweet and savory sauces, relishes, spreads, and seasonings" };
        var dairyProducts = new Category { Name = "Dairy Products"};
        var grainsCereals = new Category { Name = "Grains/Cereals", Description = "Breads, crackers, pasta, and cereal" };
        
        var jefa = new Supplier { Name = "Jefa Foods", Address = "1 Jefa Way", ContactPerson = "Jefa", 
            PhoneNumber = "555-555-5555", Email = "Jefa@gmail.com"};
        var stary = new Supplier { Name = "stary deliveries", Address = "11 stary stairs", 
            ContactPerson = "stary Drownly", PhoneNumber = "666-666-5454", Email = "stary@hotmail.com"};
        
        
        var products = new Product[]
        {
            new Product
            {
                Name = "Chai", 
                Description = "Spicy chai tea", 
                Price = 18.00M, 
                Supplier = jefa, 
                Categories = new List<Category>
                {
                    dairyProducts,
                    grainsCereals
                }
            },
            new Product
            {
                Name = "Chang", 
                Description = "An alcoholic beverage", 
                Price = 19.00M, 
                Supplier = stary, 
                Categories = new List<Category>
                {
                    beverages
                }
            },
            new Product
            {
                Name = "Aniseed Syrup", 
                Description = "Sweet syrup", 
                Price = 10.00M, 
                Supplier = jefa, 
                Categories = new List<Category>
                {
                    condiments
                }
            }
        };
        
        context.Products.AddRange(products);
        context.SaveChanges();
    }
}