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
    public class PaymentsController : ControllerBase
    {
        private readonly ClinicContext _context;
        private readonly IMapper _mapper;

        public PaymentsController(ClinicContext context, IMapper mapper)
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
            Payment? payment = await _context.Payments.SingleOrDefaultAsync(v => v.Id == id);
            if (payment == null)
                return NotFound();
            PaymentDto paymentDto = _mapper.Map<PaymentDto>(payment);
            return Ok(paymentDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentDto paymentDto)
        {
            Payment payment = _mapper.Map<Payment>(paymentDto);
            payment.Id = 0;
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            PaymentDto resultDto = _mapper.Map<PaymentDto>(payment);
            return CreatedAtAction(nameof(Get), new { id = resultDto.Id }, resultDto);
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
