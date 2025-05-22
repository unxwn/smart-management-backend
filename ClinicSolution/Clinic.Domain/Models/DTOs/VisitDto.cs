using Clinic.Domain.Models.Entities;

namespace Clinic.Domain.Models.DTOs
{
    public class VisitDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime VisitDate { get; set; }
        public string? Notes { get; set; }
        public ICollection<PrescriptionDto> Prescriptions { get; set; } = new List<PrescriptionDto>();
    }
}
