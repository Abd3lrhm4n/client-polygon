using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public record GridStateDto 
    (
        int skip,
        int take,
        FilterDescriptor filter,
        List<SortDescriptor>? sort = null
    );

    public record FilterDescriptor
    (
         string logic,
         List<FilterCondition?>? filters = null
    );

    public record FilterCondition
    (
        string field,
        string @operator,
        object value
    );

    public record SortDescriptor
    (
         string field,
         string dir
    );
}
