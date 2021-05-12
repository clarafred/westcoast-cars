using System.Collections.Generic;
using System.Threading.Tasks;
using API.DocumentEntities;
using API.Interfaces;

namespace API.Data
{
    public class ValuationRepository : IValuationRepository
    {
        private readonly CosmosContext _context;
        public ValuationRepository(CosmosContext context)
        {
            _context = context;
        }

        public void Add(Valuation valuation)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Valuation valuation)
        {
            throw new System.NotImplementedException();
        }

        public Task<Valuation> GetValuationByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Valuation> GetValuationByRegNoAsync(string regNo)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Valuation>> GetValuationsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Valuation>> GetValuationsByMakeAsync(string make)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Valuation>> GetValuationsByStatusAsync(string status)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SaveAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Valuation valuation)
        {
            throw new System.NotImplementedException();
        }
    }
}