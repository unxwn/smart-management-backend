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
    public class PaymentController : ControllerBase
    {
        private readonly ClinicContext _context;
        private readonly IMapper _mapper;

        public PaymentController(ClinicContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Payment> payments = await _context.Payments.ToListAsync();
            List<PaymentDto> paymentDtos = _mapper.Map<List<PaymentDto>>(payments);
            return Ok(paymentDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Payment? payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return NotFound();
            PaymentDto paymentDto = _mapper.Map<PaymentDto>(payment);
            return Ok(paymentDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Payment paymentDto)
        {
            Payment payment = _mapper.Map<Payment>(paymentDto);
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = paymentDto.Id }, paymentDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PaymentDto paymentDto)
        {
            if (id != paymentDto.Id)
                return BadRequest();

            Payment? payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return NotFound();

            _mapper.Map(paymentDto, payment);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Payment? payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return NotFound();

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
