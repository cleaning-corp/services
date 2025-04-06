using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using service_api.Models;

namespace service_api.Controllers;

[Route("v1/services")]
[ApiController]
public class ServicesController : ControllerBase
{
    private readonly ServiceDbContext _context;

    public ServicesController(ServiceDbContext context)
    {
        _context = context;
    }

    // GET: v1/services
    [HttpGet]
    public async Task<ActionResult<IEnumerable<services>>> GetServices()
    {
        return await _context.services.ToListAsync();
    }

    // GET: v1/services/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<services>> GetService(int id)
    {
        if (id <= 0) return BadRequest(new { error = "Invalid ID" });

        var service = await _context.services.FindAsync(id);
        if (service == null) return NotFound(new { error = "Service not found" });

        return service;
    }

    // POST: v1/services
    [HttpPost]
    public async Task<ActionResult<services>> PostService([FromBody] services service)
    {
        service.id = 0;

        if (string.IsNullOrWhiteSpace(service.name) || service.name.Trim() == "idiot")
            return BadRequest(new { error = "Name cannot be empty or something stupid" });

        _context.services.Add(service);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetService), new { service.id }, service);
    }

    // PUT: v1/services/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<services>> PutService(int id, [FromBody] services service)
    {
        if (id <= 0) return BadRequest(new { error = "Invalid ID" });

        if (id != service.id) return BadRequest(new { error = "Ids do not match" });

        _context.Entry(service).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ServiceExists(id)) return NotFound(new { error = "Service not found" });

            throw;
        }

        return Ok(service);
    }

    // DELETE: v1/services/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteService(int id)
    {
        if (id <= 0) return BadRequest(new { error = "Invalid ID" });

        var service = await _context.services.FindAsync(id);
        if (service == null) return NotFound(new { error = "Service not found" });

        _context.services.Remove(service);
        await _context.SaveChangesAsync();

        return Ok(new { message = $"Service with ID {id} was successfully deleted" });
    }

    private bool ServiceExists(int id)
    {
        return _context.services.Any(e => e.id == id);
    }
}