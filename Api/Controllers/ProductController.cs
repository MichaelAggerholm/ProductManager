using Api.Models;
using Api.Services;
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
    public IEnumerable<Product> GetAll()
    {
        return _service.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = _service.GetById(id);
        
        if (product is not null)
        {
            return product;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Create(Product newProduct)
    {
        var product = _service.Create(newProduct);
        return CreatedAtAction(nameof(GetById), new { id = product!.Id }, product);
    }
    
    [HttpPut("{id}/addcategory")]
    public IActionResult AddCategory(int id, int categoryId)
    {
        var productToUpdate = _service.GetById(id);
        
        if (productToUpdate is not null)
        {
            _service.AddCategory(id, categoryId);
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }
    
    [HttpPut("{id}/updatesupplier")]
    public IActionResult UpdateSupplier(int id, int supplierId)
    {
        var productToUpdate = _service.GetById(id);
        
        if (productToUpdate is not null)
        {
            _service.UpdateSupplier(id, supplierId);
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var productToDelete = _service.GetById(id);
        
        if (productToDelete is not null)
        {
            _service.DeleteById(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}