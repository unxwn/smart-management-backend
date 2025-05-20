using Clinic.Domain.Entities;
using Clinic.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VisitsController : ControllerBase
    {
        private readonly ClinicContext _context;
        public VisitsController(ClinicContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.Visits.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.Visits.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Visit visit)
        {
            _context.Visits.Add(visit);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = visit.Id }, visit);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Visit visit)
        {
            if (id != visit.Id) return BadRequest();
            _context.Entry(visit).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Visits.FindAsync(id);
            if (item == null) return NotFound();
            _context.Visits.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
