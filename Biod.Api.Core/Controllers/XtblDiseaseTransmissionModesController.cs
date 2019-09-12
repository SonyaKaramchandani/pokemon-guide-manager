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
    /// Cross Table Disease Transmission Modes Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/XtblDiseaseTransmissionModes")]
    public class XtblDiseaseTransmissionModesController : Controller
    {
        private readonly BiodZebraContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="XtblDiseaseTransmissionModesController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public XtblDiseaseTransmissionModesController(BiodZebraContext context)
        {
            _context = context;
        }

        // GET: api/XtblDiseaseTransmissionModes
        /// <summary>
        /// Gets the XTBL disease transmission mode.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<XtblDiseaseTransmissionMode> GetXtblDiseaseTransmissionMode()
        {
            return _context.XtblDiseaseTransmissionMode;
        }

        // GET: api/XtblDiseaseTransmissionModes/5
        /// <summary>
        /// Gets the XTBL disease transmission mode by disease identifier.
        /// </summary>
        /// <param name="id">The disease identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetXtblDiseaseTransmissionMode([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var xtblDiseaseTransmissionMode = await _context.XtblDiseaseTransmissionMode.SingleOrDefaultAsync(m => m.DiseaseId == id);

            if (xtblDiseaseTransmissionMode == null)
            {
                return NotFound();
            }

            return Ok(xtblDiseaseTransmissionMode);
        }

        //// PUT: api/XtblDiseaseTransmissionModes/5
        ///// <summary>
        ///// Puts the XTBL disease transmission mode.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <param name="xtblDiseaseTransmissionMode">The XTBL disease transmission mode.</param>
        ///// <returns></returns>
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutXtblDiseaseTransmissionMode([FromRoute] int id, [FromBody] XtblDiseaseTransmissionMode xtblDiseaseTransmissionMode)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != xtblDiseaseTransmissionMode.DiseaseId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(xtblDiseaseTransmissionMode).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!XtblDiseaseTransmissionModeExists(id))
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

        //// POST: api/XtblDiseaseTransmissionModes
        ///// <summary>
        ///// Posts the XTBL disease transmission mode.
        ///// </summary>
        ///// <param name="xtblDiseaseTransmissionMode">The XTBL disease transmission mode.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> PostXtblDiseaseTransmissionMode([FromBody] XtblDiseaseTransmissionMode xtblDiseaseTransmissionMode)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.XtblDiseaseTransmissionMode.Add(xtblDiseaseTransmissionMode);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (XtblDiseaseTransmissionModeExists(xtblDiseaseTransmissionMode.DiseaseId))
        //        {
        //            return new StatusCodeResult(StatusCodes.Status409Conflict);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetXtblDiseaseTransmissionMode", new { id = xtblDiseaseTransmissionMode.DiseaseId }, xtblDiseaseTransmissionMode);
        //}

        //// DELETE: api/XtblDiseaseTransmissionModes/5
        ///// <summary>
        ///// Deletes the XTBL disease transmission mode.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <returns></returns>
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteXtblDiseaseTransmissionMode([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var xtblDiseaseTransmissionMode = await _context.XtblDiseaseTransmissionMode.SingleOrDefaultAsync(m => m.DiseaseId == id);
        //    if (xtblDiseaseTransmissionMode == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.XtblDiseaseTransmissionMode.Remove(xtblDiseaseTransmissionMode);
        //    await _context.SaveChangesAsync();

        //    return Ok(xtblDiseaseTransmissionMode);
        //}

        //private bool XtblDiseaseTransmissionModeExists(int id)
        //{
        //    return _context.XtblDiseaseTransmissionMode.Any(e => e.DiseaseId == id);
        //}
    }
}