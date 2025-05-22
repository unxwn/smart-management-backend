using AutoMapper;
using Clinic.Domain.Models.DTOs;
using Clinic.Domain.Models.Entities;
using Clinic.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DoctorsController : ControllerBase
    {
        private readonly ClinicContext _context;
        private readonly IMapper _mapper;

        public DoctorsController(ClinicContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Doctor> doctors = await _context.Doctors
                .Include(v => v.Visits)
                .Include(v => v.Appointments)
                .ToListAsync();
            List<DoctorDto> doctorDtos = _mapper.Map<List<DoctorDto>>(doctors);
            return Ok(doctorDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Doctor? doctor = await _context.Doctors
                .Include(v => v.Visits)
                .Include(v => v.Appointments)
                .SingleOrDefaultAsync(v => v.Id == id);
            if (doctor == null)
                return NotFound();
            DoctorDto doctorDto = _mapper.Map<DoctorDto>(doctor);
            return Ok(doctorDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DoctorDto doctorDto)
        {
            Doctor doctor = _mapper.Map<Doctor>(doctorDto);
            doctor.Id = 0;
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            DoctorDto resultDto = _mapper.Map<DoctorDto>(doctor);
            return CreatedAtAction(nameof(Get), new { id = resultDto.Id }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DoctorDto doctorDto)
        {
            if (id != doctorDto.Id)
                return BadRequest();

            Doctor? doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
                return NotFound();

            _mapper.Map(doctorDto, doctor);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Doctor? doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
                return NotFound();

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
