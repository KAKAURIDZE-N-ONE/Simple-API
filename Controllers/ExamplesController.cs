using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Example.Data;
using Example.Models;

namespace Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamplesController : ControllerBase
    {
        private readonly ExampleContext _context;

        public ExamplesController(ExampleContext context)
        {
            _context = context;
        }

        // GET: api/Examples
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Examples>>> GetExamples([FromQuery] string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return await _context.Examples.ToListAsync();
            }
            var filteredExamples = await _context.Examples
           .Where(p => p.Name.Contains(name))
           .ToListAsync();

            return filteredExamples;
        }

        // GET: api/Examples/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Examples>> GetExample(int id)
        {
            var example = await _context.Examples.FindAsync(id);

            if (example == null)
            {
                return NotFound();
            }

            return example;
        }

        // PUT: api/Examples/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExample(int id, Examples example)
        {
            if (id != example.Id)
            {
                return BadRequest();
            }

            _context.Entry(example).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExampleExists(id))
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

        // POST: api/Examples
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Examples>> PostExample(Examples example)
        {
            _context.Examples.Add(example);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExample", new { id = example.Id }, example);
        }

        // DELETE: api/Examples/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExample(int id)
        {
            var example = await _context.Examples.FindAsync(id);
            if (example == null)
            {
                return NotFound();
            }

            _context.Examples.Remove(example);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExampleExists(int id)
        {
            return _context.Examples.Any(e => e.Id == id);
        }
    }
}
