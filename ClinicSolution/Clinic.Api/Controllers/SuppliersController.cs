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
    public class SuppliersController : ControllerBase
    {
        private readonly ClinicContext _context;
        private readonly IMapper _mapper;

        public SuppliersController(ClinicContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Supplier> suppliers = await _context.Suppliers
                .Include(v => v.Products)
                .ToListAsync();
            List<SupplierDto> supplierDtos = _mapper.Map<List<SupplierDto>>(suppliers);
            return Ok(supplierDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Supplier? supplier = await _context.Suppliers
                .Include(v => v.Products)
                .SingleOrDefaultAsync(v => v.Id == id);
            if (supplier == null)
                return NotFound();
            SupplierDto supplierDto = _mapper.Map<SupplierDto>(supplier);
            return Ok(supplierDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SupplierDto supplierDto)
        {
            Supplier supplier = _mapper.Map<Supplier>(supplierDto);
            supplier.Id = 0;
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            SupplierDto resultDto = _mapper.Map<SupplierDto>(supplier);
            return CreatedAtAction(nameof(Get), new { id = resultDto.Id }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SupplierDto supplierDto)
        {
            if (id != supplierDto.Id)
                return BadRequest();

            Supplier? supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
                return NotFound();

            _mapper.Map(supplierDto, supplier);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Supplier? supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
                return NotFound();

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
