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
    [Route("api/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepo;
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepo;
        private readonly IVehicleModelRepository _modelRepo;
        public VehiclesController(IVehicleRepository vehicleRepo, IBrandRepository brandRepo, IVehicleModelRepository modelRepo, IMapper mapper)
        {
            _vehicleRepo = vehicleRepo;
            _brandRepo = brandRepo;
            _modelRepo = modelRepo;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            var result = await _vehicleRepo.GetVehiclesAsync();
            var vehicles = _mapper.Map<IEnumerable<VehicleViewModel>>(result);
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var result = await _vehicleRepo.GetVehicleByIdAsync(id);
            if (result == null) return NotFound($"No vehicle found with id {id}");

            var vehicle = _mapper.Map<VehicleViewModel>(result);

            return Ok(vehicle);
        }

        [HttpGet("find/{regNum}")]
        public async Task<ActionResult<Vehicle>> FindVehicle(string regNum)
        {
            var result = await _vehicleRepo.GetVehicleByRegNumAsync(regNum);
            if (result == null) return NotFound($"No vehicle found with registration number {regNum}");

            var vehicle = _mapper.Map<VehicleViewModel>(result);

            return Ok(vehicle);
        }

        [HttpPost()]
        public async Task<ActionResult> AddVehicle(AddVehicleDto model)
        {
            try
            {
                var brand = await _brandRepo.GetBrandByNameAsync(model.Brand);
                if (brand == null) return BadRequest($"Brand {model.Brand} does not excist in system");

                var vehicleModel = await _modelRepo.GetModelByNameAsync(model.Model);
                if (vehicleModel == null) return BadRequest($"Model {model.Model} does not excist in system");

                var vehicle = new Vehicle
                {
                    RegNum = model.RegNum,
                    BrandId = brand.Id,
                    ModelId = vehicleModel.Id,
                    ModelYear = model.ModelYear,
                    FuelType = model.FuelType,
                    GearType = model.GearType,
                    Color = model.Color,
                    Mileage = model.Mileage
                };

                _vehicleRepo.Add(vehicle);
                if (await _vehicleRepo.SaveAllAsync()) 
                {
                    var newVehicle = _mapper.Map<VehicleViewModel>(vehicle);
                    return StatusCode(201, newVehicle);
                    //return CreatedAtAction(nameof(GetVehicle), result);
                }    

                return StatusCode(500, "Not able to save vehicle");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateVehicle(int id, UpdateVehicleDto model)
        {
            var vehicle = await _vehicleRepo.GetVehicleByIdAsync(id);
            if (vehicle == null) return NotFound($"No vehicle found with id {id}");

            vehicle.Color = model.Color;
            vehicle.Mileage = model.Mileage;

            _vehicleRepo.Update(vehicle);
            if (await _vehicleRepo.SaveAllAsync()) return NoContent();

            return StatusCode(500, "Not able to update vehicle");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _vehicleRepo.GetVehicleByIdAsync(id);
            if (vehicle == null) return NotFound($"No vehicle found with id {id}");

            _vehicleRepo.Delete(vehicle);
            
            if (await _vehicleRepo.SaveAllAsync()) return NoContent();

            return StatusCode(500, "Not able to delete vehicle");
        }
    }
}