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
    public class DoctorController : ControllerBase
    {
        private readonly ClinicContext _context;
        private readonly IMapper _mapper;

        public DoctorController(ClinicContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Doctor> doctors = await _context.Doctors.ToListAsync();
            List<DoctorDto> doctorDtos = _mapper.Map<List<DoctorDto>>(doctors);
            return Ok(doctorDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Doctor? doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
                return NotFound();
            DoctorDto doctorDto = _mapper.Map<DoctorDto>(doctor);
            return Ok(doctorDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Doctor doctorDto)
        {
            Doctor doctor = _mapper.Map<Doctor>(doctorDto);
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = doctorDto.Id }, doctorDto);
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
