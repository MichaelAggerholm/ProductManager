using Api.DTO;
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
            return suppliers.Select(s => new SupplierDto(s.Name, s.Address, s.ContactPerson, s.PhoneNumber, s.Email));
        }
        
        [HttpGet("{id}")]
        public ActionResult<SupplierDto> GetById(int id)
        {
            var supplier = _service.GetById(id);
            
            if (supplier is not null)
            {
                return new SupplierDto(supplier.Name, supplier.Address, supplier.ContactPerson, supplier.PhoneNumber, supplier.Email);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Create(SupplierDto newSupplier)
        {
            var supplier = _service.Create(new SupplierDto(newSupplier.Name, newSupplier.Address, newSupplier.ContactPerson, newSupplier.PhoneNumber, newSupplier.Email));
            return CreatedAtAction(nameof(GetById), new { id = supplier.Id }, new SupplierDto(supplier.Name, supplier.Address, supplier.ContactPerson, supplier.PhoneNumber, supplier.Email));
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, SupplierDto updatedSupplier)
        {
            var supplierToUpdate = _service.GetById(id);

            if (supplierToUpdate is not null)
            {
                _service.Update(id, new SupplierDto(updatedSupplier.Name, updatedSupplier.Address, updatedSupplier.ContactPerson, updatedSupplier.PhoneNumber, updatedSupplier.Email));
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