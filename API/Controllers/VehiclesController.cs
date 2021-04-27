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

        //hämtar alla vehicles, endpoint: api/vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            return await _context.Vehicles.ToListAsync();
        }

        //hämtar en vehicle, endpoint: api/vehicles/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        //skapar ny vehicle
        [HttpPost()]
        public async Task<ActionResult> AddVehicle(AddNewVehicleViewModel model)
        {
            var vehicle = new Vehicle
            {
                RegistrationNumber = model.RegNum,
                VehicleName = model.Name
            };

            //lägger till vehicle till ChangeTracking
            _context.Vehicles.Add(vehicle);

            //sparar data fysiskt till databasen
            var result = await _context.SaveChangesAsync();

            //return CreatedAtAction(nameof(GetVehicle), result);
            return StatusCode(201, vehicle);
        }
    }
}