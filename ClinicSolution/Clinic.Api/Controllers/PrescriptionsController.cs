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
    public class PrescriptionController : ControllerBase
    {
        private readonly ClinicContext _context;
        private readonly IMapper _mapper;

        public PrescriptionController(ClinicContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Prescription> prescriptions = await _context.Prescriptions.ToListAsync();
            List<PrescriptionDto> prescriptionDtos = _mapper.Map<List<PrescriptionDto>>(prescriptions);
            return Ok(prescriptionDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Prescription? prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
                return NotFound();
            PrescriptionDto prescriptionDto = _mapper.Map<PrescriptionDto>(prescription);
            return Ok(prescriptionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PrescriptionDto prescriptionDto)
        {
            Prescription prescription = _mapper.Map<Prescription>(prescriptionDto);
            prescription.Id = 0;
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            PrescriptionDto resultDto = _mapper.Map<PrescriptionDto>(prescription);
            return CreatedAtAction(nameof(Get), new { id = resultDto.Id }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PrescriptionDto prescriptionDto)
        {
            if (id != prescriptionDto.Id)
                return BadRequest();

            Prescription? prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
                return NotFound();

            _mapper.Map(prescriptionDto, prescription);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Prescription? prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
                return NotFound();

            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
