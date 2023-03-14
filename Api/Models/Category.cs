using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }

    [JsonIgnore]
    public ICollection<Product>? Products { get; set; }
}