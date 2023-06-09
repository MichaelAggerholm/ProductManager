﻿using System.ComponentModel.DataAnnotations;

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

    public Supplier Supplier { get; set; }

    public List<Category> Categories { get; set; }
    
    public Product(string name, string description, decimal price, Supplier supplier, List<Category> categories)
    {
        Name = name;
        Description = description;
        Price = price;
        Supplier = supplier;
        Categories = categories;
    }
}