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
public class PetsController : ControllerBase
{
    private readonly WorldDbContext _context;

    public PetsController(WorldDbContext context)
    {
        _context = context;
    }

    // GET: api/Pets
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pet>>> GetPets()
    {
        if (_context.Pets == null)
        {
            return NotFound();
        }

        return await _context.Pets.Include(p => p.Owner).ToListAsync();
    }

    // GET: api/Pets/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Pet>> GetPet(int id)
    {
        var pet = await _context.Pets.Include(p => p.Owner).FirstOrDefaultAsync(p => p.Id == id);

        if (pet == null)
        {
            return NotFound();
        }

        return pet;
    }

    // PUT: api/Pets/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPet(int id, Pet pet)
    {
        if (!this.ModelState.IsValid)
            return BadRequest(ModelState);

        if (id != pet.Id)
        {
            return BadRequest();
        }

        _context.Update(pet);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PetExists(id))
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

    // POST: api/Pets
    [HttpPost]
    public async Task<ActionResult<Pet>> PostPet(CreatePetDto petDto)
    {
        var pet = new Pet() { Name = petDto.Name, Race= petDto.Race };

        var owner = await _context.People.FindAsync(petDto.OwnerId);
        if (owner == null)
            return NotFound("Owner not found");
        pet.Owner = owner;

        if (!this.ModelState.IsValid)
            return BadRequest(ModelState);

        if (_context.Pets == null)
        {
            return Problem("Entity set 'PetDbContext.Pets'  is null.");
        }

        _context.Pets.Add(pet);
        await _context.SaveChangesAsync();


        return CreatedAtAction("GetPet", new { id = pet.Id }, pet);
    }

    // DELETE: api/Pets/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePet(int id)
    {
        if (_context.Pets == null)
        {
            return NotFound();
        }
        var pet = await _context.Pets.FindAsync(id);
        if (pet == null)
        {
            return NotFound();
        }

        _context.Pets.Remove(pet);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PetExists(int id)
    {
        return (_context.Pets?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
