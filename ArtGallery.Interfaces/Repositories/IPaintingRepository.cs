using ArtGallery.Data.Models;

namespace ArtGallery.Interfaces.Repositories
{
    public interface IPaintingRepository
    {
        Task<Painting?> GetByIdAsync(int id);
        Task<IEnumerable<Painting>> GetAllAsync();
        Task AddAsync(Painting painting);
        Task UpdateAsync(Painting painting);
        Task DeleteAsync(int id);
        
    }
}