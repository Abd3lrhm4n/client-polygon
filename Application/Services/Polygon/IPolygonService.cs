using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Polygon
{
    public interface IPolygonService
    {
        Task AddAsync(CreatePolygonDto entity);
        //IQueryable<Polygon> GetQueryable();
        //Task<IEnumerable<Polygon>> GetAllAsync();
        //Task<Polygon?> GetByIdAsync(string id);
        //Task AddAsync(Polygon entity);
    }
}
