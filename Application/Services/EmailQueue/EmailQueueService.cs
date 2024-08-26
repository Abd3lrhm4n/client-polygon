using Domain.Dtos;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.EmailQueue
{
    public class EmailQueueService : IEmailQueueService
    {
        private readonly IEmailQueueRepository _clientRepository;

        public EmailQueueService(IEmailQueueRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task AddBluckAsync(List<Domain.Entities.EmailQueue> emailQueues)
        {
            await _clientRepository.AddBluckAsync(emailQueues);
        }

        public async Task<List<SendingEmailQueueDto>> GetEmailsAsync()
        {
            return (await _clientRepository
                .GetEmailsAsync())
                .Select(x => new SendingEmailQueueDto
                (
                    x.UserEmail,
                    x.RequestData
                )).ToList();
        }

        public async Task UpdateBulkAsync(List<Domain.Entities.EmailQueue> emailQueues)
        {
            await _clientRepository.UpdateBulkAsync(emailQueues);
        }
    }
}
