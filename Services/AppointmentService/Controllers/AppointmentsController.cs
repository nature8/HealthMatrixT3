using AppointmentService.DTOs;
using AppointmentService.Messaging;
using AppointmentService.Models;
using AppointmentService.Repositories;
using BuildingBlocks.Contracts.Events;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentService.Controllers;

[ApiController]
[Route("api/appointments")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentRepository _repository;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<AppointmentsController> _logger;

    public AppointmentsController(
        IAppointmentRepository repository,
        IEventPublisher eventPublisher,
        ILogger<AppointmentsController> logger)
    {
        _repository = repository;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<Appointment>>> GetAll()
    {
        var appointments = await _repository.GetAllAsync();
        return Ok(appointments);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Appointment>> GetById(Guid id)
    {
        var appointment = await _repository.GetByIdAsync(id);
        if (appointment is null) return NotFound();
        return Ok(appointment);
    }

    [HttpPost]
    public async Task<ActionResult<Appointment>> Create(BookAppointmentRequest request)
    {
        var appointment = new Appointment
        {
            PatientId = request.PatientId,
            DoctorId = request.DoctorId,
            AppointmentDate = request.AppointmentDate
        };

        var created = await _repository.CreateAsync(appointment);
        _logger.LogInformation("Appointment Created: {AppointmentId}", created.AppointmentId);

        await _eventPublisher.PublishAsync(new AppointmentCreatedEvent(
            created.AppointmentId,
            created.PatientId,
            created.DoctorId,
            request.PatientEmail,
            created.AppointmentDate
        ), exchangeName: "appointment-created");

        return CreatedAtAction(nameof(GetById), new { id = created.AppointmentId }, created);
    }

    [HttpPut("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id, [FromQuery] string patientEmail)
    {
        var cancelled = await _repository.CancelAsync(id);
        if (!cancelled) return NotFound();

        _logger.LogInformation("Appointment Cancelled: {AppointmentId}", id);

        await _eventPublisher.PublishAsync(new AppointmentCancelledEvent(id, patientEmail), exchangeName: "appointment-cancelled");

        return Ok(new { message = "Appointment cancelled" });
    }
}
