namespace Clinic.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
        public int AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod Method { get; set; }
    }

    public enum PaymentMethod
    {
        Cash,
        Card,
        Online
    }
}
