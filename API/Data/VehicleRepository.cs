using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using API.ViewModels;
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

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
        {
            return await _context.Vehicles
            .Include(c => c.Brand)
            .Include(c => c.Model)
            .ToListAsync();
        }
        public async Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            return await _context.Vehicles
            .Include(c => c.Brand)
            .Include(c => c.Model)
            .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<VehicleViewModel> GetVehicleByRegNumAsync(string regNum)
        {
            return await _context.Vehicles
            .Include(c => c.Brand)
            .Include(c => c.Model)
            .Select(v => new VehicleViewModel{
                Id = v.Id,
                RegNum = v.RegNum,
                Brand = v.Brand.Name,
                Model = v.Model.Description,
                ModelYear = v.ModelYear,
                FuelType = v.FuelType,
                GearType = v.GearType,
                Color = v.Color,
                Mileage = v.Mileage
            })
            .SingleOrDefaultAsync(c => c.RegNum == regNum);
        }

    }
}