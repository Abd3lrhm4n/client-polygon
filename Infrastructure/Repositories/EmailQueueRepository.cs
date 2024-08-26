using Domain.Dtos;
using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EmailQueueRepository : IEmailQueueRepository
    {
		private readonly ApplicationDbContext _context;
        public EmailQueueRepository(ApplicationDbContext context)
		{
			_context = context;
		}

        public async Task AddBluckAsync(List<EmailQueue> emailQueues)
        {
			try
			{
				await _context.AddRangeAsync(emailQueues);
				await _context.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw new Exception("Something went wrong while adding Email to Queue");
			}
        }

		public async Task UpdateBulkAsync(List<EmailQueue> emailQueues)
		{
			try
			{
				_context.UpdateRange(emailQueues);
				await _context.SaveChangesAsync();
			}
			catch (Exception)
			{
                throw new Exception("Something went wrong while updating Email in Queue");
            }
		}

		public async Task<List<SendingEmailQueueDto>> GetEmailsAsync()
		{
			var emails = await _context.Set<EmailQueue>()
				.Join(_context.Set<Client>(),
						email => email.UserId,
						client => client.Id,
						(email, client) => new { email, client })
				.Join(_context.Set<Polygon>(),
						email => email.email.RequestId,
						polygon => polygon.Id,
						(email, polygon) => new {email, polygon})
                .Where(x => !x.email.email.IsSent)
				.Select(x => new SendingEmailQueueDto
				(
					x.email.client.Email,
					x.polygon.RequestData
				))
				.ToListAsync();

			return emails;
		}
	}
}
