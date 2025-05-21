namespace Clinic.Domain.Models.Entities
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ContactInfo { get; set; } = null!;
        public string? Address { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
