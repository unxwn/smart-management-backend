namespace Clinic.Domain.Models.DTOs
{
    public class PrescriptionDto
    {
        public int Id { get; set; }
        public int VisitId { get; set; }
        public string Medication { get; set; } = null!;
        public string Dosage { get; set; } = null!;
    }
}
