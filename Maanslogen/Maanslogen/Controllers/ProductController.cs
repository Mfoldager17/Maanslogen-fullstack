using Maanslogen.Businesslogic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Maanslogen.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController(ApplicationDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await context.Products.Include(p => p.Manufacturer).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await context.Products.Include(p => p.Manufacturer).FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            var manufacturer = await context.Manufacturers.FirstOrDefaultAsync(m => m.Id == product.ManufacturerId);
            
            if (manufacturer == null)
            {
                return BadRequest("Manufacturer not found");
            }

            if (product.Manufacturer != null)
            {
                return BadRequest("Manufacturer should be null and found by id");
            }

            product.Manufacturer = manufacturer;
            
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
    }
}