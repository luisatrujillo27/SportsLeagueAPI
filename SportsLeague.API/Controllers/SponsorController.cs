using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;

namespace SportsLeague.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SponsorController : ControllerBase
{
    private readonly LeagueDbContext _context;

    public SponsorController(LeagueDbContext context)
    {
        _context = context;
    }

    // GET: api/Sponsor
    [HttpGet]
    public async Task<IActionResult> GetSponsors()
    {
        var sponsors = await _context.Sponsors.ToListAsync();
        return Ok(sponsors);
    }

    // GET: api/Sponsor/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSponsor(int id)
    {
        var sponsor = await _context.Sponsors.FindAsync(id);
        if (sponsor == null) return NotFound();
        return Ok(sponsor);
    }

    // POST: api/Sponsor
    [HttpPost]
    public async Task<IActionResult> CreateSponsor(Sponsor sponsor)
    {
        _context.Sponsors.Add(sponsor);
        await _context.SaveChangesAsync();
        return Ok(sponsor);
    }

    // PUT: api/Sponsor/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSponsor(int id, Sponsor sponsor)
    {
        if (id != sponsor.Id) return BadRequest();

        _context.Entry(sponsor).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Sponsor/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSponsor(int id)
    {
        var sponsor = await _context.Sponsors.FindAsync(id);
        if (sponsor == null) return NotFound();

        _context.Sponsors.Remove(sponsor);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
