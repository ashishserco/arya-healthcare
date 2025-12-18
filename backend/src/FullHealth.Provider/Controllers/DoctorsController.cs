using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FullHealth.Provider.Data;
using FullHealth.Provider.Models;

namespace FullHealth.Provider.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly ProviderDbContext _context;

    public DoctorsController(ProviderDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors(string? specialty)
    {
        var query = _context.Doctors.AsQueryable();

        if (!string.IsNullOrEmpty(specialty))
        {
            query = query.Where(d => d.Specialty.Contains(specialty));
        }

        return await query.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Doctor>> GetDoctor(int id)
    {
        var doctor = await _context.Doctors.FindAsync(id);

        if (doctor == null)
        {
            return NotFound();
        }

        return doctor;
    }

    [HttpPost]
    public async Task<ActionResult<Doctor>> CreateDoctor(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDoctor), new { id = doctor.Id }, doctor);
    }
}
