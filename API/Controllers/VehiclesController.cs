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
        private readonly IBrandRepository _brandRepo;
        private readonly IVehicleModelRepository _modelRepo;

        public VehiclesController(IVehicleRepository vehicleRepo, IBrandRepository brandRepo, IVehicleModelRepository modelRepo)
        {
            _vehicleRepo = vehicleRepo;
            _brandRepo = brandRepo;
            _modelRepo = modelRepo;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            return Ok(await _vehicleRepo.GetVehiclesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleViewModel>> GetVehicle(int id)
        {
            //flytta notfound-logiken?
            var result = await _vehicleRepo.GetVehicleByIdAsync(id);
            if (result == null) return NotFound($"No vehicle found with id {id}");

            return Ok(result);
        }

        [HttpGet("find/{regNum}")]
        public async Task<ActionResult<Vehicle>> FindVehicle(string regNum)
        {
            //flytta notfound-logiken?
            var result = await _vehicleRepo.GetVehicleByRegNumAsync(regNum);
            if (result == null) return NotFound($"No vehicle found with registration number {regNum}");

            return Ok(result);
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
                                
                _vehicleRepo.Add(model);
                if (await _vehicleRepo.SaveAllAsync()) 
                {
                    return StatusCode(201);
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
            /*
            var vehicle = await _vehicleRepo.GetVehicleByIdAsync(id);
            if (vehicle == null) return NotFound($"No vehicle found with id {id}");

            vehicle.Color = model.Color;
            vehicle.Mileage = model.Mileage;

            _vehicleRepo.Update(vehicle);
            if (await _vehicleRepo.SaveAllAsync()) return NoContent();
            
            return StatusCode(500, "Not able to update vehicle");
            */
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVehicle(int id)
        {
            /*
            var vehicle = await _vehicleRepo.GetVehicleByIdAsync(id);
            if (vehicle == null) return NotFound($"No vehicle found with id {id}");

            _vehicleRepo.Delete(vehicle);
            
            if (await _vehicleRepo.SaveAllAsync()) return NoContent();

            return StatusCode(500, "Not able to delete vehicle");
            */
            return NoContent();
        }
    }
}