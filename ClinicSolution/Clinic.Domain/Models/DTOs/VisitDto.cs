namespace Clinic.Domain.Models.DTOs
{
    public class VisitDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime VisitDate { get; set; }
        public string? Notes { get; set; }
    }
}
