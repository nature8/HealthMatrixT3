using Microsoft.AspNetCore.Mvc;
using MedicalRecordService.DTOs;
using MedicalRecordService.Services;

namespace MedicalRecordService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordService _service;

        public MedicalRecordsController(IMedicalRecordService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMedicalRecordDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetByPatient),
                new { patientId = dto.PatientId },
                dto);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatient(int patientId)
        {
            var records = await _service.GetByPatientIdAsync(patientId);

            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var record = await _service.GetByIdAsync(id);

            if (record == null)
                return NotFound("Medical record not found.");

            return Ok(record);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateMedicalRecordDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);

            if (!updated)
                return NotFound("Medical record not found.");

            return Ok(new
            {
                Message = "Medical Record Updated Successfully"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound("Medical record not found.");

            return Ok(new
            {
                Message = "Medical Record Deleted Successfully"
            });
        }
    }
}