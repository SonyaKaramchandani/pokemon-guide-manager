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
    /// Cross Table Disease Alternate Names Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/XtblDiseaseAlternateNames")]
    public class XtblDiseaseAlternateNamesController : Controller
    {
        private readonly BiodZebraContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="XtblDiseaseAlternateNamesController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public XtblDiseaseAlternateNamesController(BiodZebraContext context)
        {
            _context = context;
        }

        // GET: api/XtblDiseaseAlternateNames
        /// <summary>
        /// Gets the name of the XTBL disease alternate.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<XtblDiseaseAlternateName> GetXtblDiseaseAlternateName()
        {
            return _context.XtblDiseaseAlternateName;
        }

        // GET: api/XtblDiseaseAlternateNames/5
        /// <summary>
        /// Gets the name of the XTBL disease alternate by disease identifier.
        /// </summary>
        /// <param name="id">The disease identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetXtblDiseaseAlternateName([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //_context.XtblDiseaseAlternateName.SingleOrDefaultAsync(m => m.DiseaseId == id);
            var xtblDiseaseAlternateName = await (from m in _context.XtblDiseaseAlternateName where m.DiseaseId == id select m).ToListAsync();
            

            if (xtblDiseaseAlternateName == null)
            {
                return NotFound();
            }

            return Ok(xtblDiseaseAlternateName);
        }

        //// PUT: api/XtblDiseaseAlternateNames/5
        ///// <summary>
        ///// Puts the name of the XTBL disease alternate.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <param name="xtblDiseaseAlternateName">Name of the XTBL disease alternate.</param>
        ///// <returns></returns>
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutXtblDiseaseAlternateName([FromRoute] int id, [FromBody] XtblDiseaseAlternateName xtblDiseaseAlternateName)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != xtblDiseaseAlternateName.DiseaseId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(xtblDiseaseAlternateName).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!XtblDiseaseAlternateNameExists(id))
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

        //// POST: api/XtblDiseaseAlternateNames
        ///// <summary>
        ///// Posts the name of the XTBL disease alternate.
        ///// </summary>
        ///// <param name="xtblDiseaseAlternateName">Name of the XTBL disease alternate.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> PostXtblDiseaseAlternateName([FromBody] XtblDiseaseAlternateName xtblDiseaseAlternateName)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.XtblDiseaseAlternateName.Add(xtblDiseaseAlternateName);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (XtblDiseaseAlternateNameExists(xtblDiseaseAlternateName.DiseaseId))
        //        {
        //            return new StatusCodeResult(StatusCodes.Status409Conflict);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetXtblDiseaseAlternateName", new { id = xtblDiseaseAlternateName.DiseaseId }, xtblDiseaseAlternateName);
        //}

        //// DELETE: api/XtblDiseaseAlternateNames/5
        ///// <summary>
        ///// Deletes the name of the XTBL disease alternate.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <returns></returns>
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteXtblDiseaseAlternateName([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var xtblDiseaseAlternateName = await _context.XtblDiseaseAlternateName.SingleOrDefaultAsync(m => m.DiseaseId == id);
        //    if (xtblDiseaseAlternateName == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.XtblDiseaseAlternateName.Remove(xtblDiseaseAlternateName);
        //    await _context.SaveChangesAsync();

        //    return Ok(xtblDiseaseAlternateName);
        //}

        //private bool XtblDiseaseAlternateNameExists(int id)
        //{
        //    return _context.XtblDiseaseAlternateName.Any(e => e.DiseaseId == id);
        //}
    }
}