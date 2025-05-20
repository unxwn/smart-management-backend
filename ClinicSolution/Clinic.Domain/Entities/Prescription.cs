namespace Clinic.Domain.Entities
{
    public class Prescription
    {
        public int Id { get; set; }
        public int VisitId { get; set; }
        public Visit Visit { get; set; } = null!;
        public string Medication { get; set; } = null!;
        public string Dosage { get; set; } = null!;
    }
}
