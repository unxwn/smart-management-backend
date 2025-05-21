using Clinic.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Infrastructure.Data
{
    public class ClinicContext : DbContext
    {
        public ClinicContext(DbContextOptions<ClinicContext> options)
            : base(options) { }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Visit> Visits => Set<Visit>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Prescription> Prescriptions => Set<Prescription>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>()
                .Property(a => a.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Payment>()
                .Property(p => p.Method)
                .HasConversion<string>();

            modelBuilder.Entity<Patient>()
                .Property(p => p.Gender)
                .HasConversion<string>();

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Медикаменти" },
                new Category { Id = 2, Name = "Обладнання" }
            );

            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { Id = 1, Name = "ТОВ Фармація", ContactInfo = "farm@farm.com" },
                new Supplier { Id = 2, Name = "МедСервіс", ContactInfo = "medserv@gmail.com", Address = "м. Київ" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Парацетамол", CategoryId = 1, SupplierId = 1, Price = 20.50m, Quantity = 100 },
                new Product { Id = 2, Name = "Тонометр", CategoryId = 2, SupplierId = 2, Price = 850.00m, Quantity = 20 }
            );

            modelBuilder.Entity<Patient>().HasData(
                new Patient { Id = 1, FirstName = "Олег", LastName = "Коваль", DateOfBirth = new DateTime(1990, 5, 12), Gender = PatientGender.Male, PhoneNumber = "+380971234567" },
                new Patient { Id = 2, FirstName = "Андрій", LastName = "Бадюк", DateOfBirth = new DateTime(1995, 1, 3), Gender = PatientGender.Male, PhoneNumber = "+380961757822" },
                new Patient { Id = 3, FirstName = "Євгеній", LastName = "Данченко", DateOfBirth = new DateTime(1980, 5, 5), Gender = PatientGender.Male, PhoneNumber = "+380681887091" },
                new Patient { Id = 4, FirstName = "Юлія", LastName = "Гриценко", DateOfBirth = new DateTime(1998, 1, 1), Gender = PatientGender.Female, PhoneNumber = "+38099321876" }
            );

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { Id = 1, FirstName = "Григорій", LastName = "Валиш", Specialty = "Офтальмолог" },
                new Doctor { Id = 2, FirstName = "Андій", LastName = "Дерун", Specialty = "Травматолог" }
            );

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment { Id = 1, PatientId = 1, DoctorId = 1, Date = DateTime.Today.AddDays(5), Status = AppointmentStatus.Confirmed },
                new Appointment { Id = 2, PatientId = 2, DoctorId = 2, Date = DateTime.Today.AddDays(5), Status = AppointmentStatus.Confirmed }
            );

            modelBuilder.Entity<Payment>().HasData(
                new Payment { Id = 1, PatientId = 1, AppointmentId = 1, Amount = 500, Method = PaymentMethod.Card },
                new Payment { Id = 2, PatientId = 2, AppointmentId = 2, Amount = 300, Method = PaymentMethod.Cash }
            );

            modelBuilder.Entity<Visit>().HasData(
                new Visit { Id = 1, PatientId = 1, DoctorId = 1, VisitDate = new DateTime(2025, 1, 1), Notes = "Плановий огляд" },
                new Visit { Id = 2, PatientId = 2, DoctorId = 2, VisitDate = new DateTime(2025, 2, 2), Notes = "Скарги на біль у горлі" },
                new Visit { Id = 3, PatientId = 3, DoctorId = 1, VisitDate = new DateTime(2025, 3, 3), Notes = "Післяопераційна консультація" },
                new Visit { Id = 4, PatientId = 4, DoctorId = 2, VisitDate = new DateTime(2025, 4, 4), Notes = "Первинний прийом" }
            );

            modelBuilder.Entity<MedicalRecord>().HasData(
                new MedicalRecord { Id = 1, PatientId = 1, RecordDate = new DateTime(2025, 1, 1), Details = "Пацієнт у доброму стані, скарг немає." },
                new MedicalRecord { Id = 2, PatientId = 2, RecordDate = new DateTime(2025, 2, 2), Details = "Застуда. Призначено полоскання та відпочинок." },
                new MedicalRecord { Id = 3, PatientId = 3, RecordDate = new DateTime(2025, 3, 3), Details = "Стан стабільний після операції." },
                new MedicalRecord { Id = 4, PatientId = 4, RecordDate = new DateTime(2025, 4, 4), Details = "Скарги на головний біль. Призначено обстеження." }
            );
        }
    }
}
