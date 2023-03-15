using Api.DTO;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly SupplierService _service;
        
        public SupplierController(SupplierService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public IEnumerable<SupplierDto> GetAll()
        {
            var suppliers = _service.GetAll();
            return suppliers.Select(s => new SupplierDto
            {
                Id = s.Id,
                Name = s.Name,
                Address = s.Address,
                ContactPerson = s.ContactPerson,
                PhoneNumber = s.PhoneNumber,
                Email = s.Email
            });
        }
        
        [HttpGet("{id}")]
        public ActionResult<SupplierDto> GetById(int id)
        {
            var supplier = _service.GetById(id);
            
            if (supplier is not null)
            {
                return new SupplierDto
                {
                    Id = supplier.Id,
                    Name = supplier.Name,
                    Address = supplier.Address,
                    ContactPerson = supplier.ContactPerson,
                    PhoneNumber = supplier.PhoneNumber,
                    Email = supplier.Email
                };
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Create(SupplierDto newSupplier)
        {
            var supplier = _service.Create(new SupplierDto
            {
                Name = newSupplier.Name,
                Address = newSupplier.Address,
                ContactPerson = newSupplier.ContactPerson,
                PhoneNumber = newSupplier.PhoneNumber,
                Email = newSupplier.Email
            });
            return CreatedAtAction(nameof(GetById), new { id = supplier.Id }, new SupplierDto
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Address = supplier.Address,
                ContactPerson = supplier.ContactPerson,
                PhoneNumber = supplier.PhoneNumber,
                Email = supplier.Email
            });
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, SupplierDto updatedSupplier)
        {
            var supplierToUpdate = _service.GetById(id);

            if (supplierToUpdate is not null)
            {
                _service.Update(id, new SupplierDto
                {
                    Id = updatedSupplier.Id,
                    Name = updatedSupplier.Name,
                    Address = updatedSupplier.Address,
                    ContactPerson = updatedSupplier.ContactPerson,
                    PhoneNumber = updatedSupplier.PhoneNumber,
                    Email = updatedSupplier.Email
                });
                return Ok();
            }

            return NotFound();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var supplierToDelete = _service.GetById(id);
            
            if (supplierToDelete is not null)
            {
                _service.DeleteById(id);
                return Ok();
            }

            return NotFound();
        }
    }
}