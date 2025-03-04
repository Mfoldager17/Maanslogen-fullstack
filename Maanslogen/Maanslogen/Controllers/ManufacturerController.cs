using Maanslogen.Businesslogic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Maanslogen.Controllers
{
    [Route("api/manufacturers")]
    [ApiController]
    public class ManufacturerController(ApplicationDbContext context) : ControllerBase
    {
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manufacturer>>> GetManufacturers()
        {
            return await context.Manufacturers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Manufacturer>> GetManufacturer(Guid id)
        {
            var manufacturer = await context.Manufacturers.FirstOrDefaultAsync(p => p.Id == id);

            if (manufacturer == null)
            {
                return NotFound();
            }

            return manufacturer;
        }

        [HttpPost]
        public async Task<ActionResult<Manufacturer>> PostManufacturer(Manufacturer manufacturer)
        {
            context.Manufacturers.Add(manufacturer);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetManufacturer), new { id = manufacturer.Id }, manufacturer);
        }
    }
}