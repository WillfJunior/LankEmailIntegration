using Domain.Core.Entities;

namespace Domain.Core.Repository
{
    public interface INotaRepository
    {
        Task<IEnumerable<Notas>> GetAllAsync();
        Task<Notas> GetByIdAsync(string id);
        Task<int> CreateAsync(Notas nota);
        Task<Notas> UpdateAsync(Notas nota);
        Task<bool> DeleteAsync(string id);
    }
}
