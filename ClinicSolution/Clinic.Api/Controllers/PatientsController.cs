﻿using AutoMapper;
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
    public class PatientsController : ControllerBase
    {
        private readonly ClinicContext _context;
        private readonly IMapper _mapper;

        public PatientsController(ClinicContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Patient> patients = await _context.Patients
                .Include(v => v.Visits)
                .Include(v => v.Appointments)
                .Include(v => v.Payments)
                .Include(v => v.MedicalRecords)
                .ToListAsync();
            List<PatientDto> patientDtos = _mapper.Map<List<PatientDto>>(patients);
            return Ok(patientDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Patient? patient = await _context.Patients
                .Include(v => v.Visits)
                .Include(v => v.Appointments)
                .Include(v => v.Payments)
                .Include(v => v.MedicalRecords)
                .SingleOrDefaultAsync(v => v.Id == id);
            if (patient == null)
                return NotFound();
            PatientDto patientDto = _mapper.Map<PatientDto>(patient);
            return Ok(patientDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PatientDto patientDto)
        {
            Patient patient = _mapper.Map<Patient>(patientDto);
            patient.Id = 0;
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            PatientDto resultDto = _mapper.Map<PatientDto>(patient);
            return CreatedAtAction(nameof(Get), new { id = resultDto.Id }, resultDto);
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
