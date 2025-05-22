using Clinic.Domain.Models.Entities;

namespace Clinic.Domain.Models.DTOs
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Specialty { get; set; } = null!;
        public string? Phone { get; set; }
        public ICollection<VisitDto> Visits { get; set; } = new List<VisitDto>();
        public ICollection<AppointmentDto> Appointments { get; set; } = new List<AppointmentDto>();
    }
}
