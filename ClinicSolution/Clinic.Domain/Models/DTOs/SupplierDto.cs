using Clinic.Domain.Models.Entities;

namespace Clinic.Domain.Models.DTOs
{
    public class SupplierDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ContactInfo { get; set; } = null!;
        public string? Address { get; set; }
        public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
