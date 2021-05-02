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
    [Route("api/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly DataContext _context;
        public VehiclesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            return await _context.Vehicles.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            
            if (vehicle == null)
            {
                return NotFound($"Sorry, no vehicle found with id {id}");
            }

            return vehicle;
        }

        [HttpGet("{regNum}")]
        public async Task<ActionResult<Vehicle>> FindVehicle(string regNum)
        {
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(c => c.RegNum == regNum);
            
            if (vehicle == null)
            {
                return NotFound($"Sorry, no vehicle found with registration number {regNum}");
            }

            return vehicle;
        }

        [HttpPost()]
        public async Task<ActionResult> AddVehicle(AddNewVehicleViewModel model)
        {
            var vehicle = new Vehicle
            {
                RegNum = model.RegNum,
                Model = model.Model,
                ModelYear = model.ModelYear,
                FuelType = model.FuelType,
                GearType = model.GearType,
                Color = model.Color,
                Mileage = model.Mileage
                //RegDate = model.RegDate
            };

            //lägger till vehicle till ChangeTracking
            _context.Vehicles.Add(vehicle);

            //sparar data fysiskt till databasen
            var result = await _context.SaveChangesAsync();

            //return CreatedAtAction(nameof(GetVehicle), result);
            return StatusCode(201, vehicle);
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
            //vehicle.RegDate = model.RegDate;

            _context.Vehicles.Update(vehicle);
            var result = await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}