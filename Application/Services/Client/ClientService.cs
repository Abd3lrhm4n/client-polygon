using Domain.IRepositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using System.ComponentModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Application.Helpers;

namespace Application.Services.Client
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task AddClientAsync(CreateClientDto dto)
        {
            var existingClient = await _clientRepository.GetByEmailAsync(dto.Email);
            if (existingClient != null)
            {
                throw new Exception("Email already exists.");
            }

            var client = new Domain.Entities.Client
            {
                Id = 0,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };

            await _clientRepository.AddAsync(client);
        }

        public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
        {
            var clients = await _clientRepository.GetAllAsync();
            return clients.Select(client => new ClientDto
            (
                client.Id,
                client.FirstName,
                client.LastName,
                client.Email,
                client.PhoneNumber
            ));
        }

        public async Task<ClientDto?> GetClientByIdAsync(long id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null) return null;

            return new ClientDto
            (
                client.Id,
                client.FirstName,
                client.LastName,
                client.Email,
                client.PhoneNumber
            );
        }

        public async Task UpdateClientAsync(long id, UpdateClientDto dto)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client == null)
            {
                throw new Exception("Client not found.");
            }

            var existingClient = _clientRepository.GetQueryable()
                .Where(x => x.Email == dto.Email && x.Id != id).FirstOrDefaultAsync();

            if (existingClient != null)
            {
                throw new Exception("Email already exists.");
            }

            client.FirstName = dto.FirstName;
            client.LastName = dto.LastName;
            client.Email = dto.Email;
            client.PhoneNumber = dto.PhoneNumber;

            await _clientRepository.UpdateAsync(client);
        }

        public async Task DeleteClientAsync(long id)
        {
            await _clientRepository.DeleteAsync(id);
        }

        public async Task<GridClient> GetClientsPaginAsync(GridStateDto gridState)
        {
            var domainGridStateDto = gridState.ToDomain();

            var clients = (await _clientRepository.GetClientsPaginAsync(domainGridStateDto))?
                    .Select(x => new ClientDto(x.Id, x.FirstName, x.LastName, x.Email, x.PhoneNumber)).ToList() ?? new List<ClientDto>();

            var totalCount = await _clientRepository.GetClientsCountAsync();

            return new GridClient(clients, totalCount);
        }
    }
}
