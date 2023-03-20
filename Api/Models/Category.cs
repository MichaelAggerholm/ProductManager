using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Api.DTO;

namespace Api.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public string Description { get; set; }

    [JsonIgnore]
    public List<Product> Products { get; set; }
    
    public Category(string name, string description)
    {
        Name = name;
        Description = description;
        Products = new List<Product>();
    }
}