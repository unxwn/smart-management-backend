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
    public class PrescriptionsController : ControllerBase
    {
        private readonly ClinicContext _context;
        public PrescriptionsController(ClinicContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.Prescriptions.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.Prescriptions.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Prescription prescription)
        {
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = prescription.Id }, prescription);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Prescription prescription)
        {
            if (id != prescription.Id) return BadRequest();
            _context.Entry(prescription).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Prescriptions.FindAsync(id);
            if (item == null) return NotFound();
            _context.Prescriptions.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
