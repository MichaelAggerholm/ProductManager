using Api.DTO;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _service;
    
    public CategoryController(CategoryService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public IEnumerable<CategoryDto> GetAll()
    {
        return _service.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<CategoryDto> GetById(int id)
    {
        var category = _service.GetById(id);

        if (category is not null)
        {
            return new CategoryDto(category.Name, category.Description);
        }

        return NotFound();
    }

    [HttpPost]
    public IActionResult Create([FromBody] CategoryDto categoryDto)
    {
        var category = _service.Create(categoryDto);
        var categoryToReturn = new CategoryDto(category.Name, category.Description);

        return CreatedAtAction(nameof(GetById), new { id = category.Id }, categoryToReturn);
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] CategoryDto updatedCategory)
    {
        if (id != updatedCategory.Id)
        {
            return BadRequest();
        }

        _service.Update(id, updatedCategory);

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(CategoryDto? id)
    {
        if (id is null)
        {
            return BadRequest();
        }

        _service.Delete(id);

        return NoContent();
    }
}