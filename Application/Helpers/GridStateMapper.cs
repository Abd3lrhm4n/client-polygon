using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class GridStateMapper
    {
        public static Domain.Dtos.GridStateDto ToDomain(this Application.Dtos.GridStateDto dto) =>
            new Domain.Dtos.GridStateDto(
                dto.skip,
                dto.take,
                dto.filter.ToDomain(),
                dto.sort?.Select(s => s.ToDomain()).ToList()
            );

        public static Domain.Dtos.FilterDescriptor ToDomain(this Application.Dtos.FilterDescriptor dto) =>
            new Domain.Dtos.FilterDescriptor(
                dto.logic,
                dto.filters?.Select(f => f?.ToDomain()).ToList()
            );

        public static Domain.Dtos.FilterCondition ToDomain(this Application.Dtos.FilterCondition dto) =>
            new Domain.Dtos.FilterCondition(dto.field, dto.@operator, dto.value);

        public static Domain.Dtos.SortDescriptor ToDomain(this Application.Dtos.SortDescriptor dto) =>
            new Domain.Dtos.SortDescriptor(dto.field, dto.dir);
    }
}
