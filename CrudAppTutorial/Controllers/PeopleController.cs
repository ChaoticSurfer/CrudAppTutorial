using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudAppTutorial;
using CrudAppTutorial.Models;
using CrudAppTutorial.Dtos;

[Route("api/[controller]")]
[ApiController]
public class PeopleController : ControllerBase
{
    private readonly WorldDbContext _context;

    public PeopleController(WorldDbContext context)
    {
        _context = context;
    }

    // GET: api/People
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
    {
        if (_context.People == null)
        {
            return NotFound();
        }

        return await _context.People.Include(p => p.Pets).ToListAsync();
    }

    // GET: api/People/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> GetPerson(int id)
    {
        var person = await _context.People.Include(p => p.Pets).FirstOrDefaultAsync(p => p.Id == id);

        if (person == null)
        {
            return NotFound();
        }

        return person;
    }

    // PUT: api/People/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754     DTOs
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPerson(int id, Person person)
    {
        if (!this.ModelState.IsValid)
            return BadRequest(ModelState);

        if (id != person.Id)
        {
            return BadRequest();
        }

        _context.Update(person);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PersonExists(id))
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

    // POST: api/People
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754     DTOs
    [HttpPost]
    public async Task<ActionResult<Person>> PostPerson(PersonDto personDto)
    {
        var mapper = new Mapper();
        var person = mapper.DtoToPerson(personDto);

        if (!this.ModelState.IsValid)
            return BadRequest(ModelState);


        if (_context.People == null)
        {
            return Problem("Entity set 'PersonDbContext.People'  is null.");
        }
        _context.People.Add(person);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPerson", new { id = person.Id }, person);
    }

    // DELETE: api/People/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        if (_context.People == null)
        {
            return NotFound();
        }
        var person = await _context.People.FindAsync(id);
        if (person == null)
        {
            return NotFound();
        }

        _context.People.Remove(person);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PersonExists(int id)
    {
        return (_context.People?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
