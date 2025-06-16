using ArtGallery.Data.Models;

namespace ArtGallery.Interfaces.Repositories
{
    public interface IPaintingMovementRepository
    {
        Task<PaintingMovement?> GetByIdAsync(int id);
        Task<IEnumerable<PaintingMovement>> GetAllAsync();
        Task AddAsync(PaintingMovement paintingMovement);
        Task UpdateAsync(PaintingMovement paintingMovement);
        Task DeleteAsync(int id);
    }
}