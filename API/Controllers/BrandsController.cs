using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/brands")]
    public class BrandsController : ControllerBase
    {
        private readonly DataContext _context;
        public BrandsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands() 
        {
            return await _context.Brands.ToListAsync();
        }

        [HttpPost()]
        public async Task<ActionResult> AddBrand(AddNewBrandViewModel model)
        {
            var brandResult = await _context.Brands.FirstOrDefaultAsync(c => c.Name.ToLower() == model.Name.ToLower());

            if (brandResult != null)
            {
                return BadRequest("Brand is already in the system");
            }

            var brand = new Brand
            {
                Name = model.Name
            };
            
            _context.Brands.Add(brand);

            var result = await _context.SaveChangesAsync();

            return StatusCode(201, brand);
        }

        //update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBrand(int id, AddNewBrandViewModel model)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound($"Sorry, no brand found with id {id}");
            }

            brand.Name = model.Name;

            _context.Brands.Update(brand);
            var result = await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}