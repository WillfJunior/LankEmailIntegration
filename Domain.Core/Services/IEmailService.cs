using Domain.Core.Entities;

namespace Domain.Core.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(Notas notas);
    }
}
