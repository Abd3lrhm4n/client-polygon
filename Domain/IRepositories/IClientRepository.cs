using Domain.Dtos;
using Domain.Entities;
using Domain.IRepositories.Common;

namespace Domain.IRepositories
{
    public interface IClientRepository : IRepository<Client, long>
    {
        Task<Client?> GetByEmailAsync(string email);
        Task<List<Client>> GetClientsPaginAsync(GridStateDto gridState);
        Task<long> GetClientsCountAsync();
    }
}
