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
    /// Symptoms Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/Symptoms")]
    public class SymptomsController : Controller
    {
        private readonly BiodZebraContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SymptomsController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public SymptomsController(BiodZebraContext context)
        {
            _context = context;
        }

        // GET: api/Symptoms
        /// <summary>
        /// Gets the symptoms.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Symptoms> GetSymptoms()
        {
            return _context.Symptoms;
        }

        // GET: api/Symptoms/5
        /// <summary>
        /// Gets symptom by symptoms identifier.
        /// </summary>
        /// <param name="id">The symptoms identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSymptoms([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var symptoms = await _context.Symptoms.SingleOrDefaultAsync(m => m.SymptomId == id);

            if (symptoms == null)
            {
                return NotFound();
            }

            return Ok(symptoms);
        }

        //// PUT: api/Symptoms/5
        ///// <summary>
        ///// Puts the symptoms.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <param name="symptoms">The symptoms.</param>
        ///// <returns></returns>
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSymptoms([FromRoute] int id, [FromBody] Symptoms symptoms)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != symptoms.SymptomId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(symptoms).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SymptomsExists(id))
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

        //// POST: api/Symptoms
        ///// <summary>
        ///// Posts the symptoms.
        ///// </summary>
        ///// <param name="symptoms">The symptoms.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> PostSymptoms([FromBody] Symptoms symptoms)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Symptoms.Add(symptoms);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (SymptomsExists(symptoms.SymptomId))
        //        {
        //            return new StatusCodeResult(StatusCodes.Status409Conflict);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetSymptoms", new { id = symptoms.SymptomId }, symptoms);
        //}

        //// DELETE: api/Symptoms/5
        ///// <summary>
        ///// Deletes the symptoms.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <returns></returns>
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSymptoms([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var symptoms = await _context.Symptoms.SingleOrDefaultAsync(m => m.SymptomId == id);
        //    if (symptoms == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Symptoms.Remove(symptoms);
        //    await _context.SaveChangesAsync();

        //    return Ok(symptoms);
        //}

        //private bool SymptomsExists(int id)
        //{
        //    return _context.Symptoms.Any(e => e.SymptomId == id);
        //}
    }
}