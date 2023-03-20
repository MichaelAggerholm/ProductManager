namespace Api.DTO;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public SupplierDto? Supplier { get; set; }
    public List<CategoryDto> Categories { get; set; }
    
    public ProductDto(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }
}