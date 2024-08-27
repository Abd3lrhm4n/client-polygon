using Domain.Dtos;
using Domain.Entities;
using Domain.Helper;
using Domain.IRepositories;
using Infrastructure.Context;
using Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ClientRepository : Repository<Client, long>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Client?> GetByEmailAsync(string email)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<long> GetClientsCountAsync()
        {
            return await _context.Clients.LongCountAsync();
        }

        public async Task<List<Client>> GetClientsPaginAsync(GridStateDto gridState)
        {
            var query = _context.Clients.AsQueryable();

            if (gridState.Filter != null)
            {
                foreach (var filter in gridState.Filter.Filters)
                {
                    if (filter.Field == "firstName" && filter.Value != null)
                    {
                        query = query.Where(c => c.FirstName.Contains(filter.Value.ToString()!));
                    }

                    if (filter.Field == "lastName" && filter.Value != null)
                    {
                        query = query.Where(c => c.LastName.Contains(filter.Value.ToString()!));
                    }

                    if (filter.Field == "email" && filter.Value != null)
                    {
                        query = query.Where(c => c.Email.Contains(filter.Value.ToString()!));
                    }

                    if (filter.Field == "phoneNumber" && filter.Value != null)
                    {
                        query = query.Where(c => c.PhoneNumber.Contains(filter.Value.ToString()!));
                    }
                }
            }

            if (gridState.Sort != null && gridState.Sort.Any())
            {
                foreach (var sortDescriptor in gridState.Sort)
                {
                    if (sortDescriptor.Dir == "asc")
                    {
                        query = query.OrderByDynamic(sortDescriptor.Field, true);
                    }
                    else if (sortDescriptor.Dir == "desc")
                    {
                        query = query.OrderByDynamic(sortDescriptor.Field, false);
                    }
                }
            }

            var clients = query.Skip(gridState.Skip).Take(gridState.Take).ToList();

            return clients;
        }
    }
}
