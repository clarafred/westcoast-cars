using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using API.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/brands")]
    public class BrandsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IBrandRepository _brandRepo;
        private readonly IMapper _mapper;
        public BrandsController(DataContext context, IBrandRepository brandRepo, IMapper mapper)
        {
            _context = context;
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            var result = await _brandRepo.GetBrandsAsync();
            var brands = _mapper.Map<IEnumerable<BrandViewModel>>(result);
            return Ok(brands);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Brand>> GetBrandByName(string name)
        {
            var result = await _brandRepo.GetBrandByNameAsync(name);
            if (result == null) return NotFound($"Brand {name} does not excist in system");
            
            var brand = _mapper.Map<BrandViewModel>(result);

            return Ok(brand);
        }

        [HttpPost()]
        public async Task<ActionResult> AddBrand(AddBrandDto model)
        {
            try
            {
                var brandResult = await _context.Brands.FirstOrDefaultAsync(c => c.Name.ToLower() == model.Name.ToLower());
                if (brandResult != null) return BadRequest("Brand is already in the system");

                var brand = new Brand
                {
                    Name = model.Name
                };

                _context.Brands.Add(brand);

                var result = await _context.SaveChangesAsync();

                return StatusCode(201, brand);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBrand(int id, AddBrandDto model)
        {
            try
            {
                var brand = await _context.Brands.FindAsync(id);
                if (brand == null) return NotFound($"Sorry, no brand found with id {id}");

                var nameResult = await _context.Brands.FirstOrDefaultAsync(c => c.Name.ToLower() == model.Name.ToLower());
                if (nameResult == null) return BadRequest("Brand is already in the system");

                brand.Name = model.Name;
                _context.Brands.Update(brand);
                var result = await _context.SaveChangesAsync();

                return NoContent();
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}