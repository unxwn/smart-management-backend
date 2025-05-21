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
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ClinicContext _context;
        public MedicalRecordsController(IMapper _mapper, ClinicContext context)
        {
            this._mapper = _mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<MedicalRecord> medicalRecords = await _context.MedicalRecords.ToListAsync();
            IEnumerable<MedicalRecordDto> medicalRecordsDtos = _mapper.Map<IEnumerable<MedicalRecordDto>>(medicalRecords);
            return Ok(medicalRecordsDtos);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.MedicalRecords.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MedicalRecord record)
        {
            _context.MedicalRecords.Add(record);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = record.Id }, record);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MedicalRecord record)
        {
            if (id != record.Id) return BadRequest();
            _context.Entry(record).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.MedicalRecords.FindAsync(id);
            if (item == null) return NotFound();
            _context.MedicalRecords.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
