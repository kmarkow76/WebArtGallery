using ArtGallery.Data.Models;
using ArtGallery.Interfaces.Repositories;

namespace ArtGallery.Interfaces.ServicesInterfaces;

public interface IPaintingService : IPaintingRepository
{
    Task<IEnumerable<object>> GetPaintingsWithStatusAsync();
    Task AddWithInitialMovementAsync(Painting painting);
    Task<IEnumerable<object>> GetMovementsByPaintingIdAsync(int id);
    Task<IEnumerable<object>> GetPaintingsByGenreAsync(int genreId);
}