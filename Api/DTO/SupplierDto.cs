using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.DTO
{
    public class SupplierDto
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string ContactPerson { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}