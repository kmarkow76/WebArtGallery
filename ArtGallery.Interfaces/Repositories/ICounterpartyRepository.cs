using ArtGallery.Data.Models;

namespace ArtGallery.Interfaces.Repositories
{

    public interface ICounterpartyRepository
    {
        Task<Counterparty?> GetByIdAsync(int id);
        Task<IEnumerable<Counterparty>> GetAllAsync();
        Task AddAsync(Counterparty? counterparty);
        Task UpdateAsync(Counterparty? counterparty);
        Task DeleteAsync(int id);
    }
}