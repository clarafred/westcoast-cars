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
        private readonly DataContext _context;
        private readonly IVehicleRepository _vehicleRepo;
        private readonly IMapper _mapper;
        public VehiclesController(DataContext context, IVehicleRepository vehicleRepo, IMapper mapper)
        {
            _context = context;
            _vehicleRepo = vehicleRepo;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            var vehicles = new List<PresVehicleViewModel>();
            
            var result = await _vehicleRepo.GetVehiclesAsync();

            foreach (var vehicle in result)
            {
                var v = new PresVehicleViewModel
                    {
                    Id = vehicle.Id,
                    RegNum = vehicle.RegNum,
                    Brand = vehicle.Brand.Name,
                    Model = vehicle.Model.Description,
                    ModelYear = vehicle.ModelYear,
                    FuelType = vehicle.FuelType,
                    GearType = vehicle.GearType,
                    Color = vehicle.Color,
                    Mileage = vehicle.Mileage
                };

                vehicles.Add(v);
            }

            return Ok(vehicles);

            //return await _context.Vehicles.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var result = await _vehicleRepo.GetVehicleByIdAsync(id);
            var vehicle = _mapper.Map<PresVehicleViewModel>(result);
            return Ok(vehicle);

            /*
            if (vehicle == null)
            {
                return NotFound($"Sorry, no vehicle found with id {id}");
            }

            return vehicle;
            */
        }

        [HttpGet("find/{regNum}")]
        public async Task<ActionResult<Vehicle>> FindVehicle(string regNum)
        {
            var result = await _vehicleRepo.GetVehicleByRegNumAsync(regNum);

            /*
            if (vehicle == null)
            {
                return NotFound($"Sorry, no vehicle found with registration number {regNum}");
            }
            */

            return Ok(result);
        }

        [HttpPost()]
        public async Task<ActionResult> AddVehicle(AddNewVehicleViewModel model)
        {
            var vehicleBrand = await _context.Brands.SingleOrDefaultAsync(c => c.Name.ToLower() == model.Brand.ToLower());

            if (vehicleBrand == null)
            {
                return BadRequest($"Brand {model.Brand} does not excist in system");
            }

            var vehicleModel = await _context.VehicleModels.SingleOrDefaultAsync(c => c.Description.ToLower() == model.Model.ToLower());

            if (vehicleModel == null)
            {
                return BadRequest($"Model {model.Model} does not excist in system");
            }

            var vehicle = new Vehicle
            {
                RegNum = model.RegNum,
                BrandId = vehicleBrand.Id,
                ModelId = vehicleModel.Id,
                ModelYear = model.ModelYear,
                FuelType = model.FuelType,
                GearType = model.GearType,
                Color = model.Color,
                Mileage = model.Mileage
            };

            //lägger till vehicle till ChangeTracking
            _context.Vehicles.Add(vehicle);

            //sparar data fysiskt till databasen
            var result = await _context.SaveChangesAsync();

            //mappa till en view model för retur
            var newVehicle = new PresVehicleViewModel
            {
                RegNum = vehicle.RegNum,
                Brand = vehicleBrand.Name,
                Model = vehicleModel.Description,
                ModelYear = vehicle.ModelYear,
                Color = vehicle.Color,
            };

            //return CreatedAtAction(nameof(GetVehicle), result);
            return StatusCode(201, newVehicle);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound($"Sorry, no vehicle found with id {id}");
            }

            _context.Vehicles.Remove(vehicle);
            var result = await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateVehicle(int id, UpdateVehicleViewModel model)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound($"Sorry, no vehicle found with id {id}");
            }

            vehicle.Color = model.Color;
            vehicle.Mileage = model.Mileage;

            _context.Vehicles.Update(vehicle);
            var result = await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}