using Clinic.Domain.Models.Entities;

namespace Clinic.Domain.Models.DTOs
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int? AppointmentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod Method { get; set; }
    }
}
