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
    /// Transmission Modes Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/TransmissionModes")]
    public class TransmissionModesController : Controller
    {
        private readonly BiodZebraContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransmissionModesController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public TransmissionModesController(BiodZebraContext context)
        {
            _context = context;
        }

        // GET: api/TransmissionModes
        /// <summary>
        /// Gets the transmission modes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<TransmissionModes> GetTransmissionModes()
        {
            return _context.TransmissionModes;
        }

        // GET: api/TransmissionModes/5
        /// <summary>
        /// Gets transmission mode by transmission modes identifier.
        /// </summary>
        /// <param name="id">The transmission modes identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransmissionModes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transmissionModes = await _context.TransmissionModes.SingleOrDefaultAsync(m => m.TransmissionModeId == id);

            if (transmissionModes == null)
            {
                return NotFound();
            }

            return Ok(transmissionModes);
        }

        //// PUT: api/TransmissionModes/5
        ///// <summary>
        ///// Puts the transmission modes.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <param name="transmissionModes">The transmission modes.</param>
        ///// <returns></returns>
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTransmissionModes([FromRoute] int id, [FromBody] TransmissionModes transmissionModes)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != transmissionModes.TransmissionModeId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(transmissionModes).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TransmissionModesExists(id))
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

        //// POST: api/TransmissionModes
        ///// <summary>
        ///// Posts the transmission modes.
        ///// </summary>
        ///// <param name="transmissionModes">The transmission modes.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> PostTransmissionModes([FromBody] TransmissionModes transmissionModes)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.TransmissionModes.Add(transmissionModes);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TransmissionModesExists(transmissionModes.TransmissionModeId))
        //        {
        //            return new StatusCodeResult(StatusCodes.Status409Conflict);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetTransmissionModes", new { id = transmissionModes.TransmissionModeId }, transmissionModes);
        //}

        //// DELETE: api/TransmissionModes/5
        ///// <summary>
        ///// Deletes the transmission modes.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <returns></returns>
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTransmissionModes([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var transmissionModes = await _context.TransmissionModes.SingleOrDefaultAsync(m => m.TransmissionModeId == id);
        //    if (transmissionModes == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TransmissionModes.Remove(transmissionModes);
        //    await _context.SaveChangesAsync();

        //    return Ok(transmissionModes);
        //}

        //private bool TransmissionModesExists(int id)
        //{
        //    return _context.TransmissionModes.Any(e => e.TransmissionModeId == id);
        //}
    }
}