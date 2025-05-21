using Clinic.Domain.Models.Entities;

namespace Clinic.Domain.Models.DTOs
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime ScheduledDate { get; set; }
        public AppointmentStatus Status { get; set; }
    }

}
