using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetBrandsAsync();
        Task<Brand> GetBrandByNameAsync(string name);
    }
}