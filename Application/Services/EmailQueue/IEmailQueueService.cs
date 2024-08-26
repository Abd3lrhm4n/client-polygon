using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Dtos;

namespace Application.Services.EmailQueue
{
    public interface IEmailQueueService
    {
        Task AddBluckAsync(List<Domain.Entities.EmailQueue> emailQueues);
        Task UpdateBulkAsync(List<Domain.Entities.EmailQueue> emailQueues);
        Task<List<SendingEmailQueueDto>> GetEmailsAsync();
    }
}
