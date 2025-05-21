namespace Clinic.Domain.Models.Entities
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
        public DateTime RecordDate { get; set; }
        public string Details { get; set; } = null!;
    }
}
