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
        private readonly IBrandRepository _brandRepo;
        private readonly IMapper _mapper;
        public BrandsController(IBrandRepository brandRepo, IMapper mapper)
        {
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
                var brandResult = await _brandRepo.GetBrandByNameAsync(model.Name);
                if (brandResult != null) return BadRequest("Brand is already in the system");

                var brand = new Brand
                {
                    Name = model.Name
                };

                _brandRepo.Add(brand);

                if (await _brandRepo.SaveAllAsync()) return StatusCode(201, brand);

                return StatusCode(500, "Not able to save brand");
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
                var brand = await _brandRepo.GetBrandByIdAsync(id);
                if (brand == null) return NotFound($"No brand found with id {id}");

                //var nameResult = await _context.Brands.FirstOrDefaultAsync(c => c.Name.ToLower() == model.Name.ToLower());
                //if (nameResult == null) return BadRequest("Brand is already in the system");

                brand.Name = model.Name;
                _brandRepo.Update(brand);

                if (await _brandRepo.SaveAllAsync()) return NoContent();

                return StatusCode(500, "Not able to update brand");
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}