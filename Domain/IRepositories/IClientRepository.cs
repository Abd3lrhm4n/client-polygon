using Domain.Entities;
using Domain.IRepositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IClientRepository : IRepository<Client, long>
    {
        Task<Client?> GetByEmailAsync(string email);
    }
}
