using ArtGallery.Data.Models;

namespace ArtGallery.Interfaces.Repositories
{
    public interface IRentalRepository
    {
        Task<Rental?> GetByIdAsync(int id);
        Task<IEnumerable<Rental>> GetAllAsync();
        Task AddAsync(Rental rental);
        Task UpdateAsync(Rental rental);
        Task DeleteAsync(int id);
    }
}