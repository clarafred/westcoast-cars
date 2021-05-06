using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly DataContext _context;
        public VehicleRepository(DataContext context)
        {
            _context = context;
        }

        public Task<Vehicle> GetVehicleById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Vehicle> GetVehicleByRegNumAsync(string regNum)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
        {
            return await _context.Vehicles
                .Include(c => c.Brand)
                .Include(c => c.Model)
                .ToListAsync();
        }
    }
}