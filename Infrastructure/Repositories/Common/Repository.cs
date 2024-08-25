using Domain.Entities.Common;
using Domain.IRepositories.Common;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Repositories.Common
{
    public class Repository<TR, TE> : IRepository<TR, TE> where TR : BaseEntity<TE>
    {
        protected readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TR entity)
        {
            await _context.Set<TR>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TE id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<TR>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TR>> GetAllAsync()
        {
            return await _context.Set<TR>().ToListAsync();
        }

        public async Task<TR?> GetByIdAsync(TE id)
        {
            return await _context.Set<TR>().FindAsync(id);
        }

        public IQueryable<TR> GetQueryable()
        {
            return _context.Set<TR>();
        }

        public async Task UpdateAsync(TR entity)
        {
            _context.Set<TR>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
