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
        return _service.GetAll();
    }
    
    
    [HttpGet("{id}")]
    public ActionResult<ProductDto> GetById(int id)
    {
        var product = _service.GetById(id);

        if (product is not null)
        {
            return _service.MapToDto(product);
        }

        return NotFound();
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] ProductDto productDto)
    {
        var createdProduct = _service.Create(productDto);

        var createdProductDto = new ProductDto(createdProduct.Name, createdProduct.Description, createdProduct.Price);

        return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProductDto);
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
    public IActionResult Delete(ProductDto? id)
    {
        if (id is null)
        {
            return BadRequest();
        }

        _service.Delete(id);

        return NoContent();
    }
}