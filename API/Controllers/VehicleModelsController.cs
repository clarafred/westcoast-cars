using System;
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
    [Route("api/vehiclemodels")]
    public class VehicleModelsController : ControllerBase
    {
        private readonly DataContext _context;
        public VehicleModelsController(DataContext context)
        {
            _context = context;
        }
        
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<VehicleModel>>> GetVehicleModels() 
        {
            return await _context.VehicleModels.ToListAsync();
        }

        [HttpPost()]
        public async Task<ActionResult> AddVehicleModel(AddNewVehicleModelViewModel model)
        {
            try
            {
                var vehicleModel = new VehicleModel
                {
                    Description = model.Description
                };

                _context.VehicleModels.Add(vehicleModel);

                var result = await _context.SaveChangesAsync();

                return StatusCode(200, vehicleModel);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}