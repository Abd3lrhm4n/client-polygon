using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Client
{
    public interface IClientService
    {
        Task AddClientAsync(CreateClientDto dto);
        Task<IEnumerable<ClientDto>> GetAllClientsAsync();
        Task<ClientDto?> GetClientByIdAsync(long id);
        Task UpdateClientAsync(long id, UpdateClientDto dto);
        Task DeleteClientAsync(long id);
    }
}
