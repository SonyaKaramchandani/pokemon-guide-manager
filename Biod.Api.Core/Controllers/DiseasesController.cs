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
    /// Diseases Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/Diseases")]
    public class DiseasesController : Controller
    {
        private readonly BiodZebraContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiseasesController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DiseasesController(BiodZebraContext context)
        {
            _context = context;
        }

        // GET: api/Diseases
        /// <summary>
        /// Gets the diseases.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Diseases> GetDiseases()
        {
            return _context.Diseases;
        }

        // GET: api/Diseases/5
        /// <summary>
        /// Gets disease by disease identifier.
        /// </summary>
        /// <param name="id">The disease identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiseases([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var diseases = await _context.Diseases.SingleOrDefaultAsync(m => m.DiseaseId == id);

            if (diseases == null)
            {
                return NotFound();
            }

            return Ok(diseases);
        }

        //// PUT: api/Diseases/5
        ///// <summary>
        ///// Puts the diseases.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <param name="diseases">The diseases.</param>
        ///// <returns></returns>
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDiseases([FromRoute] int id, [FromBody] Diseases diseases)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != diseases.DiseaseId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(diseases).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DiseasesExists(id))
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

        //// POST: api/Diseases
        ///// <summary>
        ///// Posts the diseases.
        ///// </summary>
        ///// <param name="diseases">The diseases.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> PostDiseases([FromBody] Diseases diseases)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Diseases.Add(diseases);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (DiseasesExists(diseases.DiseaseId))
        //        {
        //            return new StatusCodeResult(StatusCodes.Status409Conflict);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetDiseases", new { id = diseases.DiseaseId }, diseases);
        //}

        //// DELETE: api/Diseases/5
        ///// <summary>
        ///// Deletes the diseases.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <returns></returns>
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteDiseases([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var diseases = await _context.Diseases.SingleOrDefaultAsync(m => m.DiseaseId == id);
        //    if (diseases == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Diseases.Remove(diseases);
        //    await _context.SaveChangesAsync();

        //    return Ok(diseases);
        //}

        //private bool DiseasesExists(int id)
        //{
        //    return _context.Diseases.Any(e => e.DiseaseId == id);
        //}
    }
}