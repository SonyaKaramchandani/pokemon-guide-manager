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
    /// Cross Table Disease Symptoms Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/XtblDiseaseSymptoms")]
    public class XtblDiseaseSymptomsController : Controller
    {
        private readonly BiodZebraContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="XtblDiseaseSymptomsController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public XtblDiseaseSymptomsController(BiodZebraContext context)
        {
            _context = context;
        }

        // GET: api/XtblDiseaseSymptoms
        /// <summary>
        /// Gets the XTBL disease symptom.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<XtblDiseaseSymptom> GetXtblDiseaseSymptom()
        {
            return _context.XtblDiseaseSymptom;
        }

        // GET: api/XtblDiseaseSymptoms/5
        /// <summary>
        /// Gets the XTBL disease symptom by disease identifier.
        /// </summary>
        /// <param name="id">The disease identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetXtblDiseaseSymptom([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var xtblDiseaseSymptom = await (from m in _context.XtblDiseaseSymptom where m.DiseaseId == id select m).ToListAsync();

            if (xtblDiseaseSymptom == null)
            {
                return NotFound();
            }

            return Ok(xtblDiseaseSymptom);
        }

        //// PUT: api/XtblDiseaseSymptoms/5
        ///// <summary>
        ///// Puts the XTBL disease symptom.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <param name="xtblDiseaseSymptom">The XTBL disease symptom.</param>
        ///// <returns></returns>
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutXtblDiseaseSymptom([FromRoute] int id, [FromBody] XtblDiseaseSymptom xtblDiseaseSymptom)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != xtblDiseaseSymptom.DiseaseId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(xtblDiseaseSymptom).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!XtblDiseaseSymptomExists(id))
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

        //// POST: api/XtblDiseaseSymptoms
        ///// <summary>
        ///// Posts the XTBL disease symptom.
        ///// </summary>
        ///// <param name="xtblDiseaseSymptom">The XTBL disease symptom.</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> PostXtblDiseaseSymptom([FromBody] XtblDiseaseSymptom xtblDiseaseSymptom)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.XtblDiseaseSymptom.Add(xtblDiseaseSymptom);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (XtblDiseaseSymptomExists(xtblDiseaseSymptom.DiseaseId))
        //        {
        //            return new StatusCodeResult(StatusCodes.Status409Conflict);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetXtblDiseaseSymptom", new { id = xtblDiseaseSymptom.DiseaseId }, xtblDiseaseSymptom);
        //}

        //// DELETE: api/XtblDiseaseSymptoms/5
        ///// <summary>
        ///// Deletes the XTBL disease symptom.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <returns></returns>
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteXtblDiseaseSymptom([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var xtblDiseaseSymptom = await _context.XtblDiseaseSymptom.SingleOrDefaultAsync(m => m.DiseaseId == id);
        //    if (xtblDiseaseSymptom == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.XtblDiseaseSymptom.Remove(xtblDiseaseSymptom);
        //    await _context.SaveChangesAsync();

        //    return Ok(xtblDiseaseSymptom);
        //}

        //private bool XtblDiseaseSymptomExists(int id)
        //{
        //    return _context.XtblDiseaseSymptom.Any(e => e.DiseaseId == id);
        //}
    }
}