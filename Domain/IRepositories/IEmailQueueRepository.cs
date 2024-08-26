using Domain.Dtos;
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IEmailQueueRepository
    {
        Task AddBluckAsync(List<EmailQueue> emailQueues);
        Task UpdateBulkAsync(List<EmailQueue> emailQueues);
        Task<List<SendingEmailQueueDto>> GetEmailsAsync();
    }
}
