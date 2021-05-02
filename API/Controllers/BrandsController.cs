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
            var brand = new Brand
            {
                Name = model.Name
            };
            
            _context.Brands.Add(brand);

            var result = await _context.SaveChangesAsync();

            return StatusCode(201, brand);
        }

        //update
    }
}