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
    public class VisitController : ControllerBase
    {
        private readonly ClinicContext _context;
        private readonly IMapper _mapper;

        public VisitController(ClinicContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Visit> visits = await _context.Visits.ToListAsync();
            List<VisitDto> visitDtos = _mapper.Map<List<VisitDto>>(visits);
            return Ok(visitDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Visit? visit = await _context.Visits.FindAsync(id);
            if (visit == null)
                return NotFound();
            VisitDto visitDto = _mapper.Map<VisitDto>(visit);
            return Ok(visitDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Visit visitDto)
        {
            Visit visit = _mapper.Map<Visit>(visitDto);
            _context.Visits.Add(visit);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = visitDto.Id }, visitDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VisitDto visitDto)
        {
            if (id != visitDto.Id)
                return BadRequest();

            Visit? visit = await _context.Visits.FindAsync(id);
            if (visit == null)
                return NotFound();

            _mapper.Map(visitDto, visit);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Visit? visit = await _context.Visits.FindAsync(id);
            if (visit == null)
                return NotFound();

            _context.Visits.Remove(visit);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

