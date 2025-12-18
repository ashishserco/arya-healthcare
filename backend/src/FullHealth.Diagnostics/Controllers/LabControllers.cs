using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FullHealth.Diagnostics.Data;
using FullHealth.Diagnostics.Models;

namespace FullHealth.Diagnostics.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LabPackagesController : ControllerBase
{
    private readonly DiagnosticsDbContext _context;

    public LabPackagesController(DiagnosticsDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LabPackage>>> GetPackages(string? category)
    {
        var query = _context.LabPackages.AsQueryable();

        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(p => p.Category == category);
        }

        return await query.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LabPackage>> GetPackage(int id)
    {
        var package = await _context.LabPackages.FindAsync(id);
        if (package == null) return NotFound();
        return package;
    }

    [HttpPost]
    public async Task<ActionResult<LabPackage>> CreatePackage(LabPackage package)
    {
        _context.LabPackages.Add(package);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPackage), new { id = package.Id }, package);
    }
}

[ApiController]
[Route("api/[controller]")]
public class LabTestsController : ControllerBase
{
    private readonly DiagnosticsDbContext _context;

    public LabTestsController(DiagnosticsDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LabTest>>> GetTests()
    {
        return await _context.LabTests.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LabTest>> GetTest(int id)
    {
        var test = await _context.LabTests.FindAsync(id);
        if (test == null) return NotFound();
        return test;
    }

    [HttpPost]
    public async Task<ActionResult<LabTest>> CreateTest(LabTest test)
    {
        _context.LabTests.Add(test);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTest), new { id = test.Id }, test);
    }
}
