using Api.Services;
using Api.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    ProductService _service;
    
    public ProductController(ProductService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public IEnumerable<ProductDto> GetAll()
    {
        var products = _service.GetAll();
        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            SupplierId = p.Supplier?.Id,
            CategoryIds = p.Categories?.Select(c => c.Id).ToList() ?? new List<int>()
        });
    }
    
    
    [HttpGet("{id}")]
    public ActionResult<ProductDto> GetById(int id)
    {
        var product = _service.GetById(id);

        if (product is not null)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                SupplierId = product.Supplier?.Id,
                CategoryIds = product.Categories.Select(c => c.Id).ToList()
            };
        }

        return NotFound();
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] ProductDto productDto)
    {
        var product = _service.Create(productDto);

        var productToReturn = new ProductDto
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            SupplierId = product.Supplier.Id,
            CategoryIds = product.Categories.Select(c => c.Id).ToList()
        };

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, productToReturn);
    }
    

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] ProductDto updatedProduct)
    {
        if (id != updatedProduct.Id)
        {
            return BadRequest();
        }

        _service.Update(id, updatedProduct);

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(ProductDto id)
    {
        if (id is null)
        {
            return BadRequest();
        }

        _service.Delete(id);

        return NoContent();
    }
}