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
    /// Pathogen Types Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/PathogenTypes")]
    public class PathogenTypesController : Controller
    {
        private readonly BiodZebraContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathogenTypesController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PathogenTypesController(BiodZebraContext context)
        {
            _context = context;
        }

        // GET: api/PathogenTypes
        /// <summary>
        /// Gets the pathogen types.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<PathogenTypes> GetPathogenTypes()
        {
            return _context.PathogenTypes;
        }

        // GET: api/PathogenTypes/5
        /// <summary>
        /// Gets pathogen type by pathogen type identifier.
        /// </summary>
        /// <param name="id">The pathogen type identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPathogenTypes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pathogenTypes = await _context.PathogenTypes.SingleOrDefaultAsync(m => m.PathogenTypeId == id);

            if (pathogenTypes == null)
            {
                return NotFound();
            }

            return Ok(pathogenTypes);
        }

        //// PUT: api/PathogenTypes/5
        ///// <summary>
        ///// Puts the pathogen types.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <param name="pathogenTypes">The pathogen types.</param>
        ///// <returns></returns>
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPathogenTypes([FromRoute] int id, [FromBody] PathogenTypes pathogenTypes)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != pathogenTypes.PathogenTypeId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(pathogenTypes).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PathogenTypesExists(id))
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

        //// POST: api/PathogenTypes
        ///// <summary>
        ///// Posts the pathogen types.
        ///// </summary>
        ///// <param name="pathogenTypes">The pathogen types.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> PostPathogenTypes([FromBody] PathogenTypes pathogenTypes)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.PathogenTypes.Add(pathogenTypes);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (PathogenTypesExists(pathogenTypes.PathogenTypeId))
        //        {
        //            return new StatusCodeResult(StatusCodes.Status409Conflict);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetPathogenTypes", new { id = pathogenTypes.PathogenTypeId }, pathogenTypes);
        //}

        //// DELETE: api/PathogenTypes/5
        ///// <summary>
        ///// Deletes the pathogen types.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <returns></returns>
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePathogenTypes([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var pathogenTypes = await _context.PathogenTypes.SingleOrDefaultAsync(m => m.PathogenTypeId == id);
        //    if (pathogenTypes == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.PathogenTypes.Remove(pathogenTypes);
        //    await _context.SaveChangesAsync();

        //    return Ok(pathogenTypes);
        //}

        //private bool PathogenTypesExists(int id)
        //{
        //    return _context.PathogenTypes.Any(e => e.PathogenTypeId == id);
        //}
    }
}