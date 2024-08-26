using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PolygonRepository : IPolygonRepository
    {
        private readonly ApplicationDbContext _context;
        public PolygonRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Polygon entity)
        {
            _context.Set<Polygon>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<Polygon>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Polygon?> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Polygon> GetQueryable()
        {
            throw new NotImplementedException();
        }
    }
}
