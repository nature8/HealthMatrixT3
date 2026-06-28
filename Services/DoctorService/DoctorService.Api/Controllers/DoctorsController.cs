using DoctorService.Api.Models;
using DoctorService.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using DoctorService.Api.DTOs;

namespace DoctorService.Api.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorRepository _repository;
    private readonly ILogger<DoctorsController> _logger;

    public DoctorsController(IDoctorRepository repository, ILogger<DoctorsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<Doctor>>> GetAll()
    {
        var doctors = await _repository.GetAllAsync();
        return Ok(doctors);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Doctor>> GetById(int id)
    {
        var doctor = await _repository.GetByIdAsync(id);
        if (doctor is null) return NotFound();
        return Ok(doctor);
    }

    [HttpPost]
    public async Task<ActionResult<Doctor>> Create(CreateDoctorRequest request)
    {
        var doctor = new Doctor
        {
            Name = request.Name,
            Department = request.Department,
            Specialization = request.Specialization,
            Experience = request.Experience,
            Fee = request.Fee
        };

        var created = await _repository.CreateAsync(doctor);
        _logger.LogInformation("Doctor Created: {DoctorId}", created.DoctorId);

        return CreatedAtAction(nameof(GetById), new { id = created.DoctorId }, created);
    }
}