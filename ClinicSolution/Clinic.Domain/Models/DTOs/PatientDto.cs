using Clinic.Domain.Models.Entities;

namespace Clinic.Domain.Models.DTOs
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public PatientGender Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public ICollection<VisitDto> Visits { get; set; } = new List<VisitDto>();
        public ICollection<AppointmentDto> Appointments { get; set; } = new List<AppointmentDto>();
        public ICollection<PaymentDto> Payments { get; set; } = new List<PaymentDto>();
        public ICollection<MedicalRecordDto> MedicalRecords { get; set; } = new List<MedicalRecordDto>();
    }
}
