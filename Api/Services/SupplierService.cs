using Api.Models;
using Api.Data;
using Api.DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class SupplierService
    {
        private readonly SupplierContext _context;

        public SupplierService(SupplierContext context)
        {
            _context = context;
        }

        public IEnumerable<SupplierDto> GetAll()
        {
            var suppliers = _context.Suppliers.AsNoTracking();
            return suppliers.AsEnumerable()
                .Select(s => new SupplierDto(s.Name, s.Address, s.ContactPerson, s.PhoneNumber, s.Email));
        }

        // Method GetById that returns a SupplierDto with the given id
        public SupplierDto? GetById(int id)
        {
            var supplier = _context.Suppliers.Find(id);

            if (supplier is not null)
            {
                return (new SupplierDto(supplier.Name, supplier.Address, supplier.ContactPerson, supplier.PhoneNumber, supplier.Email));
            }

            return null;
        }

        public SupplierDto Create(SupplierDto newSupplierDto)
        {
            var newSupplier = new Supplier
            {
                Name = newSupplierDto.Name,
                Address = newSupplierDto.Address,
                ContactPerson = newSupplierDto.ContactPerson,
                PhoneNumber = newSupplierDto.PhoneNumber,
                Email = newSupplierDto.Email
            };

            _context.Suppliers.Add(newSupplier);
            _context.SaveChanges();

            newSupplierDto.Id = newSupplier.Id;

            return newSupplierDto;
        }

        public void Update(int id, SupplierDto updatedSupplierDto)
        {
            var supplier = _context.Suppliers.Find(id);

            if (supplier is not null)
            {
                supplier.Name = updatedSupplierDto.Name;
                supplier.Address = updatedSupplierDto.Address;
                supplier.ContactPerson = updatedSupplierDto.ContactPerson;
                supplier.PhoneNumber = updatedSupplierDto.PhoneNumber;
                supplier.Email = updatedSupplierDto.Email;

                _context.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            var supplier = _context.Suppliers.Find(id);

            if (supplier is not null)
            {
                _context.Suppliers.Remove(supplier);
                _context.SaveChanges();
            }
        }
    }
}