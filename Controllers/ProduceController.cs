using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProduceInSeasonApi.Models;

namespace ProduceInSeasonApi.Controllers
{
    [Route("api/Produce")]
    [ApiController]
    public class ProduceController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProduceController(ProductContext context)
        {
            _context = context;
        }

        // GET: api/Produce
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProduce()
        {
            return await _context.Produce
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/Produce/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(long id)
        {
            var product = await _context.Produce.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return ItemToDTO(product);
        }

        // PUT: api/Produce/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(long id, ProductDTO productDTO)
        {
            if (id != productDTO.Id)
            {
                return BadRequest();
            }

            var product = await _context.Produce.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = productDTO.Name;
            product.IsFruit = productDTO.IsFruit;
            product.Description = productDTO.Description;
            product.Season = productDTO.Season;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Produce
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct(ProductDTO productDTO)
        {
            var product = new Product
            {
                IsFruit = productDTO.IsFruit,
                Name = productDTO.Name,
                Description = productDTO.Description,
                Season = productDTO.Season
            };

            _context.Produce.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, ItemToDTO(product));
        }

        // DELETE: api/Produce/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var product = await _context.Produce.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Produce.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(long id)
        {
            return _context.Produce.Any(e => e.Id == id);
        }

        private static ProductDTO ItemToDTO(Product product) => new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Season = product.Season,
            Description = product.Description,
            IsFruit = product.IsFruit
        };
    }
}
