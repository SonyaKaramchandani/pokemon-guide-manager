using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biod.Api.Core.Models;

namespace Biod.Api.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportRankingsController : ControllerBase
    {
        private readonly BiodZebraContext _context;

        public AirportRankingsController(BiodZebraContext context)
        {
            _context = context;
        }

        // GET: api/AirportRankings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirportRanking>>> GetAirportRanking()
        {
            return await _context.AirportRanking.ToListAsync();
        }

        // GET: api/AirportRankings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AirportRanking>> GetAirportRanking(int id)
        {
            var airportRanking = await _context.AirportRanking.FindAsync(id);

            if (airportRanking == null)
            {
                return NotFound();
            }

            return airportRanking;
        }

        // PUT: api/AirportRankings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAirportRanking(int id, AirportRanking airportRanking)
        {
            if (id != airportRanking.StationId)
            {
                return BadRequest();
            }

            _context.Entry(airportRanking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirportRankingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AirportRankings
        [HttpPost]
        public async Task<ActionResult<AirportRanking>> PostAirportRanking(AirportRanking airportRanking)
        {
            _context.AirportRanking.Add(airportRanking);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AirportRankingExists(airportRanking.StationId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAirportRanking", new { id = airportRanking.StationId }, airportRanking);
        }

        // DELETE: api/AirportRankings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AirportRanking>> DeleteAirportRanking(int id)
        {
            var airportRanking = await _context.AirportRanking.FindAsync(id);
            if (airportRanking == null)
            {
                return NotFound();
            }

            _context.AirportRanking.Remove(airportRanking);
            await _context.SaveChangesAsync();

            return airportRanking;
        }

        private bool AirportRankingExists(int id)
        {
            return _context.AirportRanking.Any(e => e.StationId == id);
        }
    }
}
