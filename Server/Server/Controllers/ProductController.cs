using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly SocialContext _context;

        public ProductsController(SocialContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _context.Products.ToListAsync();
            return Ok(result);
        }

        // GET api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var p = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (p== null)
            {
                return NotFound();
            }
            return Ok(p);
        }

        // POST api/products
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]Product entity)
        {
            using (var context = new SocialContext())
            {
                _context.Products.Add(entity);
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction(nameof(GetProduct), new { id = entity.ProductId }, entity);
        }

        // PUT api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync(int id, [FromBody] Product entity)
        {
            if (id != entity.ProductId)
            {
                return BadRequest();
            }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = entity.Name;
            product.Price = entity.Price;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
