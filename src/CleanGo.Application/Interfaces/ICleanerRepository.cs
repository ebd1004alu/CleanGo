using CleanGo.Domain.Entities;

namespace CleanGo.Application.Interfaces
{
    public interface ICleanerRepository
    {
        Task<Cleaner?> GetByIdAsync(Guid id);
        Task<IEnumerable<Cleaner>> GetAllAsync();
        Task AddAsync(Cleaner cleaner);
        Task UpdateAsync(Cleaner cleaner);      
        Task DeleteAsync(Cleaner cleaner);
    }
}
