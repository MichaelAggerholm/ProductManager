namespace Api.DTO;

public class CategoryDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public List<ProductDto> Products { get; set; }
    
    public CategoryDto(string name, string description)
    {
        Name = name;
        Description = description;
        Products = new List<ProductDto>();
    }
}