using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<Vehicle>> GetVehicles()
        {
            return _context.Vehicles.ToList();
        }

        //hämtar en vehicle, endpoint: api/vehicles/id
        [HttpGet("{id}")]
        public ActionResult<Vehicle> GetVehicle(int id)
        {
            return _context.Vehicles.Find(id);
        }
    }
}