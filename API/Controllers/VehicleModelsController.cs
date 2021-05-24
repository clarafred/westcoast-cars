using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/vehiclemodels")]
    public class VehicleModelsController : ControllerBase
    {
        private readonly IVehicleModelRepository _modelRepo;
        public VehicleModelsController(IVehicleModelRepository modelRepo)
        {
            _modelRepo = modelRepo;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<VehicleModel>>> GetVehicleModels()
        {
            return Ok(await _modelRepo.GetModelsAsync());
        }

        [HttpPost()]
        public async Task<ActionResult> AddVehicleModel(AddVehicleModelDto model)
        {
            try
            {
                var vehicleModel = await _modelRepo.GetModelByNameAsync(model.Description);
                if (vehicleModel != null) return BadRequest("Model is already in system");

                var newModel = new VehicleModel
                {
                    Description = model.Description
                };

                _modelRepo.Add(newModel);
                
                if (await _modelRepo.SaveAllAsync()) return StatusCode(201, newModel);

                return StatusCode(500, "Not able to save model");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateVehicleModel(int id, AddVehicleModelDto model)
        {
            var vehicleModel = await _modelRepo.GetModelByIdAsync(id);
            if (vehicleModel == null) return NotFound($"No model found with id {id}");

            //var modelName = await _context.VehicleModels.FirstOrDefaultAsync(c => c.Name.ToLower() == model.Name.ToLower());
            //if (modelName == null) return BadRequest("Model is already in the system");

            vehicleModel.Description = model.Description;
            _modelRepo.Update(vehicleModel);

            if (await _modelRepo.SaveAllAsync()) return StatusCode(201, vehicleModel);

            return StatusCode(500, "Not able to update model");
        }
    }
}