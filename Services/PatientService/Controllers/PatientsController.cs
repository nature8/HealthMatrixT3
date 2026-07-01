using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientService.Data;
using PatientService.DTOs;
using PatientService.Models;

namespace PatientService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly PatientDbContext _context;

    public PatientsController(PatientDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetPatients()
    {
        return Ok(await _context.Patients.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatient(int id)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(x => x.PatientId == id);

        if (patient == null)
            return NotFound();

        return Ok(patient);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePatient(
        CreatePatientDto dto)
    {
        var patient = new Patient
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            DateOfBirth = dto.DateOfBirth
        };

        _context.Patients.Add(patient);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetPatient),
            new { id = patient.PatientId },
            patient);
    }
}

/*
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientService.Data;
using PatientService.DTOs;
using PatientService.Models;

namespace PatientService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PatientsController : ControllerBase
{
    private readonly PatientDbContext _context;

    public PatientsController(PatientDbContext context)
    {
        _context = context;
    }

    // Admin, Doctor, Receptionist can view all patients
    [Authorize(Roles = "Admin,Doctor,Receptionist")]
    [HttpGet]
    public async Task<IActionResult> GetPatients()
    {
        var patients = await _context.Patients.ToListAsync();

        return Ok(patients);
    }

    // Admin, Doctor, Receptionist can view a patient
    [Authorize(Roles = "Admin,Doctor,Receptionist")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatient(int id)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(x => x.PatientId == id);

        if (patient == null)
        {
            return NotFound(new
            {
                Message = "Patient not found"
            });
        }

        return Ok(patient);
    }

    // Admin and Receptionist can register patients
    [Authorize(Roles = "Admin,Receptionist")]
    [HttpPost]
    public async Task<IActionResult> CreatePatient([FromBody] CreatePatientDto dto)
    {
        var existingPatient = await _context.Patients
            .FirstOrDefaultAsync(x => x.Email == dto.Email);

        if (existingPatient != null)
        {
            return BadRequest(new
            {
                Message = "Patient already exists"
            });
        }

        var patient = new Patient
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            DateOfBirth = dto.DateOfBirth,
            CreatedAt = DateTime.UtcNow
        };

        _context.Patients.Add(patient);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetPatient),
            new { id = patient.PatientId },
            patient);
    }

    // Admin and Receptionist can update patients
    [Authorize(Roles = "Admin,Receptionist")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatient(
        int id,
        CreatePatientDto dto)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(x => x.PatientId == id);

        if (patient == null)
        {
            return NotFound(new
            {
                Message = "Patient not found"
            });
        }

        patient.FirstName = dto.FirstName;
        patient.LastName = dto.LastName;
        patient.Email = dto.Email;
        patient.Phone = dto.Phone;
        patient.DateOfBirth = dto.DateOfBirth;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            Message = "Patient updated successfully"
        });
    }

    // Only Admin can delete patients
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(x => x.PatientId == id);

        if (patient == null)
        {
            return NotFound(new
            {
                Message = "Patient not found"
            });
        }

        _context.Patients.Remove(patient);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            Message = "Patient deleted successfully"
        });
    }

    // Logged-in patient can view their own profile
    [Authorize(Roles = "Patient")]
    [HttpGet("my-profile")]
    public IActionResult MyProfile()
    {
        var userId = User.Claims
            .FirstOrDefault(c =>
                c.Type ==
                System.Security.Claims.ClaimTypes.NameIdentifier)
            ?.Value;

        return Ok(new
        {
            Message = "Patient profile endpoint",
            UserId = userId
        });
    }
}*/