using Api.Models;

namespace Api.DTO;

public class ProductDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public int? SupplierId { get; set; }
    
    public List<int>? CategoryIds { get; set; }
}