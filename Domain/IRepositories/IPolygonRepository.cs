using Domain.Entities;
using Domain.IRepositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IPolygonRepository
    {
        IQueryable<Polygon> GetQueryable();
        Task<IEnumerable<Polygon>> GetAllAsync();
        Task<Polygon?> GetByIdAsync(string id);
        Task AddAsync(Polygon entity);
    }
}
