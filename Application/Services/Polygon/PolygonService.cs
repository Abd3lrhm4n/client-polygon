using Application.Dtos;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Polygon
{
    public class PolygonService : IPolygonService
    {
        private readonly IPolygonRepository _polygonRepository;

        public PolygonService(IPolygonRepository polygonRepository)
        {
            _polygonRepository = polygonRepository;
        }

        public async Task AddAsync(CreatePolygonDto entity)
        {
            await _polygonRepository.AddAsync(new Domain.Entities.Polygon
            {
                Id = entity.request_id,
                RequestData = entity.results,
            });
        }
    }
}
