using AutoMapper;
using Clinic.Domain.Models.DTOs;
using Clinic.Domain.Models.Entities;

namespace Clinic.Api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<Patient, PatientDto>().ReverseMap();
            CreateMap<Doctor, DoctorDto>().ReverseMap();
            CreateMap<Visit, VisitDto>().ReverseMap();
            CreateMap<Appointment, AppointmentDto>().ReverseMap();
            CreateMap<Prescription, PrescriptionDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<MedicalRecord, MedicalRecordDto>().ReverseMap();

            CreateMap<User, UserDto>();
            CreateMap<AuthUserDto, User>();
        }
    }
}