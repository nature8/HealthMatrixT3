using BillingService.DTOs;
using BillingService.Services;
using Microsoft.AspNetCore.Mvc;

namespace BillingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillsController : ControllerBase
    {
        private readonly IBillService _service;

        public BillsController(IBillService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBillDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.CreateAsync(dto);

            return Ok(new
            {
                Message = "Bill Created Successfully"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bills = await _service.GetAllAsync();
            return Ok(bills);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bill = await _service.GetByIdAsync(id);

            if (bill == null)
                return NotFound("Bill not found.");

            return Ok(bill);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateBillDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);

            if (!updated)
                return NotFound("Bill not found.");

            return Ok(new
            {
                Message = "Bill Updated Successfully"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound("Bill not found.");

            return Ok(new
            {
                Message = "Bill Deleted Successfully"
            });
        }
    }
}