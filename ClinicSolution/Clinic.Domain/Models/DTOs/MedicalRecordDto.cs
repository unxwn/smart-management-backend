namespace Clinic.Domain.Models.DTOs
{
    public class MedicalRecordDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime RecordDate { get; set; }
        public string Details { get; set; } = null!;
    }
}
