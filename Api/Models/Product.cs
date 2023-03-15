using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Product
{
    [Key] public int Id { get; set; }

    [Required] public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    [Range(1.00, 10000.00)]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    public Supplier? Supplier { get; set; }

    public ICollection<Category> Categories { get; set; }
    
}