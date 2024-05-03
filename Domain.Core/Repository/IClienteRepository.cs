using Domain.Core.Entities;

namespace Domain.Core.Repository
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente> GetByIdAsync(string id);
        Task<Cliente> GetByCNPJAsync(string cnpj);
        Task CreateAsync(Cliente cliente);
        Task<Cliente> UpdateAsync(Cliente cliente);
        Task<bool> DeleteAsync(string id);
    }
}
