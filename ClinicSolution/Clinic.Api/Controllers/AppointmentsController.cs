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
    public class AppointmentsController : ControllerBase
    {
        private readonly ClinicContext _context;
        private readonly IMapper _mapper;

        public AppointmentsController(ClinicContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Appointment> appointments = await _context.Appointments.ToListAsync();
            List<AppointmentDto> appointmentDtos = _mapper.Map<List<AppointmentDto>>(appointments);
            return Ok(appointmentDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Appointment? appointment = await _context.Appointments.SingleOrDefaultAsync(v => v.Id == id);
            if (appointment == null)
                return NotFound();
            AppointmentDto appointmentDto = _mapper.Map<AppointmentDto>(appointment);
            return Ok(appointmentDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppointmentDto appointmentDto)
        {
            Appointment appointment = _mapper.Map<Appointment>(appointmentDto);
            appointment.Id = 0;
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            AppointmentDto resultDto = _mapper.Map<AppointmentDto>(appointment);
            return CreatedAtAction(nameof(Get), new { id = resultDto.Id }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AppointmentDto appointmentDto)
        {
            if (id != appointmentDto.Id)
                return BadRequest();

            Appointment? appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            _mapper.Map(appointmentDto, appointment);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Appointment? appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
