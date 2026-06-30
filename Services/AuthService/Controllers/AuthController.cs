using AuthService.Data;
using AuthService.DTOs;
using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthDbContext _context;

    private readonly JwtService _jwtService;

    public AuthController(AuthDbContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (_context.Users.Any(x => x.Email == dto.Email))
        {
            return BadRequest("Email already exists");
        }

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = dto.Role
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            Message = "User registered successfully"
        });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        var user = _context.Users
            .FirstOrDefault(x => x.Email == dto.Email);

        if (user == null)
        {
            return Unauthorized("Invalid email or password");
        }

        bool validPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

        if (!validPassword)
        {
            return Unauthorized("Invalid email or password");
        }

        var token = _jwtService.GenerateToken(user);

        return Ok(new
        {
            Token = token,
            User = new
            {
                user.Id,
                user.Name,
                user.Email,
                user.Role
            }
        });
    }

    [Authorize]
    [HttpGet("profile")]
    public IActionResult Profile()
    {
        return Ok(new
        {
            Message = "Authenticated user"
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admins-only")]
    public IActionResult AdminOnly()
    {
        return Ok(new
        {
            Message = "Welcome Admin"
        });
    }

    [Authorize(Roles = "Doctor")]
    [HttpGet("doctors-only")]
    public IActionResult DoctorOnly()
    {
        return Ok(new
        {
            Message = "Welcome Doctor"
        });
    }

    [Authorize(Roles = "Patient")]
    [HttpGet("patients-only")]
    public IActionResult PatientOnly()
    {
        return Ok(new
        {
            Message = "Welcome Patient"
        });
    }
}