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
    public class PatientController : ControllerBase
    {
        private readonly ClinicContext _context;
        private readonly IMapper _mapper;

        public PatientController(ClinicContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Patient> patients = await _context.Patients.ToListAsync();
            List<PatientDto> patientDtos = _mapper.Map<List<PatientDto>>(patients);
            return Ok(patientDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Patient? patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return NotFound();
            PatientDto patientDto = _mapper.Map<PatientDto>(patient);
            return Ok(patientDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Patient patientDto)
        {
            Patient patient = _mapper.Map<Patient>(patientDto);
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = patientDto.Id }, patientDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PatientDto patientDto)
        {
            if (id != patientDto.Id)
                return BadRequest();

            Patient? patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return NotFound();

            _mapper.Map(patientDto, patient);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Patient? patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return NotFound();

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
