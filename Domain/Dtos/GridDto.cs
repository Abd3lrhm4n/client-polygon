using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public record GridStateDto
    (
        int Skip,
        int Take,
        FilterDescriptor Filter,
        List<SortDescriptor>? Sort = null
    );

    public record FilterDescriptor
    (
         string Logic,
         List<FilterCondition?>? Filters = null
    );

    public record FilterCondition
    (
        string Field,
        string Operator,
        object Value
    );

    public record SortDescriptor
    (
         string Field,
         string Dir
    );
}
