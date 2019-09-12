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
    /// <summary>
    /// Systems Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/Systems")]
    public class SystemsController : Controller
    {
        private readonly BiodZebraContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemsController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public SystemsController(BiodZebraContext context)
        {
            _context = context;
        }

        // GET: api/Systems
        /// <summary>
        /// Gets the systems.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Systems> GetSystems()
        {
            return _context.Systems;
        }

        // GET: api/Systems/5
        /// <summary>
        /// Gets system by systems identifier.
        /// </summary>
        /// <param name="id">The systems identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSystems([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var systems = await _context.Systems.SingleOrDefaultAsync(m => m.SystemId == id);

            if (systems == null)
            {
                return NotFound();
            }

            return Ok(systems);
        }

        //// PUT: api/Systems/5
        ///// <summary>
        ///// Puts the systems.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <param name="systems">The systems.</param>
        ///// <returns></returns>
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSystems([FromRoute] int id, [FromBody] Systems systems)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != systems.SystemId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(systems).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SystemsExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Systems
        ///// <summary>
        ///// Posts the systems.
        ///// </summary>
        ///// <param name="systems">The systems.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> PostSystems([FromBody] Systems systems)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Systems.Add(systems);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (SystemsExists(systems.SystemId))
        //        {
        //            return new StatusCodeResult(StatusCodes.Status409Conflict);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetSystems", new { id = systems.SystemId }, systems);
        //}

        //// DELETE: api/Systems/5
        ///// <summary>
        ///// Deletes the systems.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <returns></returns>
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSystems([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var systems = await _context.Systems.SingleOrDefaultAsync(m => m.SystemId == id);
        //    if (systems == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Systems.Remove(systems);
        //    await _context.SaveChangesAsync();

        //    return Ok(systems);
        //}

        //private bool SystemsExists(int id)
        //{
        //    return _context.Systems.Any(e => e.SystemId == id);
        //}
    }
}