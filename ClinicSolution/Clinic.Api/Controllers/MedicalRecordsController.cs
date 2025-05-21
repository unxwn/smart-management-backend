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
    public class MedicalRecordController : ControllerBase
    {
        private readonly ClinicContext _context;
        private readonly IMapper _mapper;

        public MedicalRecordController(ClinicContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<MedicalRecord> medicalRecords = await _context.MedicalRecords.ToListAsync();
            List<MedicalRecordDto> medicalRecordDtos = _mapper.Map<List<MedicalRecordDto>>(medicalRecords);
            return Ok(medicalRecordDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            MedicalRecord? medicalRecord = await _context.MedicalRecords.FindAsync(id);
            if (medicalRecord == null)
                return NotFound();
            MedicalRecordDto medicalRecordDto = _mapper.Map<MedicalRecordDto>(medicalRecord);
            return Ok(medicalRecordDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MedicalRecord medicalRecordDto)
        {
            MedicalRecord medicalRecord = _mapper.Map<MedicalRecord>(medicalRecordDto);
            _context.MedicalRecords.Add(medicalRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = medicalRecordDto.Id }, medicalRecordDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MedicalRecordDto medicalRecordDto)
        {
            if (id != medicalRecordDto.Id)
                return BadRequest();

            MedicalRecord? medicalRecord = await _context.MedicalRecords.FindAsync(id);
            if (medicalRecord == null)
                return NotFound();

            _mapper.Map(medicalRecordDto, medicalRecord);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            MedicalRecord? medicalRecord = await _context.MedicalRecords.FindAsync(id);
            if (medicalRecord == null)
                return NotFound();

            _context.MedicalRecords.Remove(medicalRecord);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
