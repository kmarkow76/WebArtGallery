using ArtGallery.Data.Models;

namespace ArtGallery.Interfaces.Repositories
{
    public interface IPainterRepository
    {
        Task<Painter?> GetByIdAsync(int id);
        Task<IEnumerable<Painter>> GetAllAsync();
        Task AddAsync(Painter painter);
        Task UpdateAsync(Painter painter);
        Task DeleteAsync(int id);
        Task<Painter?> GetByFullNameAsync(string firstname, string lastname);
    }
}