using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.Common
{
    public interface IRepository<TR, TE> where TR : BaseEntity<TE>
    {
        IQueryable<TR> GetQueryable();
        Task<IEnumerable<TR>> GetAllAsync();
        Task<TR?> GetByIdAsync(TE id);
        Task AddAsync(TR entity);
        Task UpdateAsync(TR entity);
        Task DeleteAsync(TE id);
    }
}
