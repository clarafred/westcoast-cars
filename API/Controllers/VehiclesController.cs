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
            var result = await _vehicleRepo.GetVehiclesAsync();
            var vehicles = _mapper.Map<IEnumerable<PresVehicleViewModel>>(result);
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
            */
        }

        [HttpGet("find/{regNum}")]
        public async Task<ActionResult<Vehicle>> FindVehicle(string regNum)
        {
            var result = await _vehicleRepo.GetVehicleByRegNumAsync(regNum);
            var vehicle = _mapper.Map<PresVehicleViewModel>(result);
            return Ok(vehicle);

            /*
            if (vehicle == null)
            {
                return NotFound($"Sorry, no vehicle found with registration number {regNum}");
            }
            */
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
            var  newVehicle = _mapper.Map<PresVehicleViewModel>(vehicle);

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