using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerplexIdeeenbus.Models;

namespace PerplexIdeeenbus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdeasController : ControllerBase
    {
        private readonly IdeaContext _context;

        public IdeasController(IdeaContext context)
        {
            _context = context;
        }

        // GET: api/Ideas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Idea>>> GetIdeas()
        {
            return await _context.Ideas.ToListAsync();
        }

        // GET: api/Ideas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Idea>> GetIdea(long id)
        {
            var idea = await _context.Ideas.FindAsync(id);

            if (idea == null)
            {
                return NotFound("Entry with Id " + id + " does not exist.");
            }

            return idea;
        }

        // PUT: api/Ideas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIdea(long id, Idea idea)
        {
            if (!IdeaExists(id))
            {
                return NotFound("Entry with Id " + id + " does not exist.");
            }

            if (id != idea.Id)
            {
                return BadRequest("Cannot change an existing Id");
            }

            _context.Entry(idea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdeaExists(id))
                {
                    return NotFound("Entry with Id " + id + " does not exist.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Ideas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Idea>> PostIdea(Idea idea)
        {
            /* Pseudocode idee voor datumvereiste. Momenteel wordt bij afwezigheid van begin- en einddatum in het request een default waarde van '0001-01-01 00:00' toegevoegd.
             * Om het onderstaande goed the implementeren zou het daarom ideaal zijn als de begin- en einddatum nullable zijn, maar deze aanpassing is mij niet op tijd gelukt.
             *
             * if (idea.Type == IdeaType.uitje)
             *      if (idea.BeginDatum == null)
             *          return BadRequest("BeginDatum required for Type uitje");
             *      if (idea.EindDatum == null)
             *          return BadRequest("EindDatum required for Type uitje");           
             */
            _context.Ideas.Add(idea);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIdea), new { id = idea.Id }, idea);
        }

        // DELETE: api/Ideas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIdea(long id)
        {
            var idea = await _context.Ideas.FindAsync(id);
            if (idea == null)
            {
                return NotFound("Entry with Id " + id + " does not exist.");
            }

            _context.Ideas.Remove(idea);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IdeaExists(long id)
        {
            return _context.Ideas.Any(e => e.Id == id);
        }
    }
}
